using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Reklama.Areas.CatalogAdmin.Models;
using Reklama.Data.Entities;
using Reklama.Data.Models;
using Reklama.Data.Servises;
using ExcelLibrary.SpreadSheet;

namespace Reklama.Areas.CatalogAdmin.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class ProductController : Controller
    {
        private readonly ShopsService _shopService = new ShopsService();
        private readonly ProductService _productService = new ProductService();

        //
        // GET: /CatalogAdmin/Product/

        public ActionResult Index(int id = 0)
        {
            var result = new IndexProductPageViewModel();
            result.Group = _shopService.GetGroups();
            if (id == 0)
            {
                result.CurrentID = result.Group.First().Category.First().ID;
            }
            else
            {
                result.CurrentID = id;
            }

            result.Products = _productService.GetProducts(result.CurrentID);
            return View(result);
        }

        //
        // GET: /CatalogAdmin/Product/Create

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(FormCollection collection)
        {
            var actionResult = new List<ImportResultItem>();

            var file = Request.Files[0];
            if (file == null || file.ContentLength == 0 || String.IsNullOrEmpty(file.FileName) ||
                !file.FileName.Contains(".xls"))
            {
                actionResult.Add(new ImportResultItem
                {
                    ResultType = ImportResultType.CommonError,
                    Message = "Can't import. File extension must be <strong>.xls</strong>"
                });
                return View(actionResult);
            }

            var workbook = Workbook.Load(file.InputStream);
            var worksheet = workbook.Worksheets[0];

            var caellCategoryID = worksheet.Cells[0, 0];
            int categoryID;
            if (caellCategoryID.IsEmpty || !int.TryParse(caellCategoryID.StringValue, out categoryID))
            {
                actionResult.Add(new ImportResultItem
                {
                    ResultType = ImportResultType.CommonError,
                    Message = "Can't import. You shouldn't edit the first two rows"
                });
                return View(actionResult);
            }

            var rowsIndexs = worksheet.Cells.Rows.Where(q => q.Key > 1).Select(q => q.Key).OrderBy(q => q);
            var lastColumn = worksheet.Cells.Rows[0].LastColIndex;

            var result = new List<LocalProduct>();

            foreach (var rowIndex in rowsIndexs)
            {
                var tempLocalProduct = new LocalProduct();

                var sProductID = worksheet.Cells[rowIndex, 0].Value != null
                    ? (string) worksheet.Cells[rowIndex, 0].Value
                    : "";

                var product = new Product();

                int productID;
                if (int.TryParse(sProductID, out productID))
                {
                    product.ID = productID;
                }

                product.CategoryID = categoryID;
                product.ReviewLink = worksheet.Cells[rowIndex, 1].StringValue;
                product.Title = worksheet.Cells[rowIndex, 2].StringValue;
                product.SmallDescription = worksheet.Cells[rowIndex, 3].StringValue;
                product.Description = worksheet.Cells[rowIndex, 4].StringValue;

                var isActive = worksheet.Cells[rowIndex, 5].StringValue != "" &&
                                worksheet.Cells[rowIndex, 5].StringValue == "1";

                product.IsActive = isActive;

                var manufacturerID = 0;
                if (!int.TryParse(worksheet.Cells[rowIndex, 6].StringValue, out manufacturerID)) continue;
                product.ManufacturerID = manufacturerID;

                tempLocalProduct.Product = product;

                for (var j = 7; j <= lastColumn; j++)
                {
                    if (worksheet.Cells[rowIndex, j].IsEmpty) continue;

                    int tempCategoryParameterID;
                    if (int.TryParse(worksheet.Cells[0, j].StringValue, out tempCategoryParameterID))
                    {
                        tempLocalProduct.ProductValues.Add(new ProductParameterValue
                        {
                            Value = worksheet.Cells[rowIndex, j].ToString(),
                            CategoryParameterID = tempCategoryParameterID
                        });
                    }
                }

                result.Add(tempLocalProduct);
            }

            actionResult = _productService.ImportProducts(result);

            return View(actionResult);
        }


        public void Export(int id)
        {
            var products = _productService.GetProducts(id);
            var category = _shopService.GetCategory(id);

            if (category == null) return;

            var workbook = new Workbook();
            var worksheet = new Worksheet("First Sheet");
            for (var k = 0; k < 200; k++)
            {
                worksheet.Cells[k, 0] = new Cell(null);
            }

            worksheet.Cells[0, 0] = new Cell(category.ID.ToString(CultureInfo.InvariantCulture));

            worksheet.Cells[1, 0] = new Cell("ID");
            worksheet.Cells[1, 1] = new Cell("ReviewLink");
            worksheet.Cells[1, 2] = new Cell("Title");
            worksheet.Cells[1, 3] = new Cell("SmallDescription");
            worksheet.Cells[1, 4] = new Cell("Description");
            worksheet.Cells[1, 5] = new Cell("IsActive");
            worksheet.Cells[1, 6] = new Cell("Производитель");

            var sections = category.CategoryParametersSection.OrderBy(w => w.Name);
            var q = 7;
            foreach (var section in sections)
            {
                foreach (var p in section.CategoryParameter.OrderBy(w => w.Name))
                {
                    worksheet.Cells[0, q] = new Cell(p.ID);
                    worksheet.Cells[1, q] = new Cell(p.Name);
                    q++;
                }
            }


            var i = 2;

            foreach (var product in products)
            {
                var tempParameters = _productService.GetProductParameters(category.ID, product.ID);


                var j = 0;
                foreach (var section in tempParameters.OrderBy(w => w.Key.Name))
                {
                    foreach (var o in section.OrderBy(w => w.Parameter.Name))
                    {
                        if (o.ParameterValue != null)
                        {
                            worksheet.Cells[i, j + 7] = new Cell(o.ParameterValue.Value);
                        }
                        j++;
                    }
                }

                worksheet.Cells[i, 0] = new Cell(product.ID.ToString(CultureInfo.InvariantCulture));
                worksheet.Cells[i, 1] = new Cell(product.ReviewLink);
                worksheet.Cells[i, 2] = new Cell(product.Title);
                worksheet.Cells[i, 3] = new Cell(product.SmallDescription);
                worksheet.Cells[i, 4] = new Cell(product.Description);
                worksheet.Cells[i, 5] = new Cell(product.IsActive ? "1" : "0");
                worksheet.Cells[i, 6] = new Cell(product.ManufacturerID);
                i++;
            }

            //worksheet.Cells[0, 1] = new Cell((short)1);
            //worksheet.Cells[2, 0] = new Cell(9999999);
            //worksheet.Cells[3, 3] = new Cell((decimal)3.45);
            //worksheet.Cells[2, 2] = new Cell("Text string");
            //worksheet.Cells[2, 4] = new Cell("Second string");
            //worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
            //worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY\-MM\-DD");

            worksheet.Cells.ColumnWidth[1] = 4000;

            workbook.Worksheets.Add(worksheet);



            var stream = new MemoryStream();
            workbook.SaveToStream(stream);
            stream.Position = 0;
            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition",
                String.Format("attachment; filename={0}_products.xls", category.Name.Replace(" ", "_")));
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        public ActionResult Edit(long id)
        {
            var result = new EditProductPageViewModel();
            var groups = _shopService.GetGroups().OrderBy(q => q.Name);
            var manufactures = _shopService.GetManufacturers().OrderBy(q => q.Name);

            if (id == 0)
            {
                var firstGroup = groups.First(q => q.Category.Any());
                var categoriesByFirstGroup = _shopService.GetCategories(firstGroup.ID).ToList();
                var firstCategoriesByFirstGroup = categoriesByFirstGroup.First();
                result.Groups = new SelectList(groups.OrderBy(q => q.Name), "ID", "Name", firstGroup);
                result.Manufacturers = new SelectList(manufactures, "ID", "Name");
                result.Categories = new SelectList(categoriesByFirstGroup.OrderBy(q => q.Name), "ID", "Name",
                    firstCategoriesByFirstGroup);
                result.Sections = _productService.GetProductParameters(firstCategoriesByFirstGroup.ID).ToList();
                return View(result);
            }

            var product = _productService.GetProduct(id);
            if (product == null) return RedirectToAction("Index");

            result.Product = product;
            result.Groups = new SelectList(groups.OrderBy(q => q.Name), "ID", "Name", product.Category.GroupID);
            result.Manufacturers = new SelectList(manufactures, "ID", "Name", product.ManufacturerID);
            result.Categories = new SelectList(
                _shopService.GetCategories(product.Category.GroupID).OrderBy(q => q.Name), "ID", "Name",
                product.CategoryID);
            result.Sections = _productService.GetProductParameters(product.CategoryID, product.ID).ToList();

            //result.Photos =
            //    product.ProductImage.Select(
            //        q =>
            //            String.Format(
            //                "<img src='/Images/Catalog/Product/{0}' class='file-preview-image' alt='The Moon' title='The Moon'>",
            //                q.ImageName)).ToArray();

            ViewBag.UploadedImages = (product.ProductImage != null) ? from image in product.ProductImage select "/Images/Catalog/Product/" + image.ImageName + ";" + image.IsTitular.ToString().ToLower() : null;

            return View(result);
        }

        //
        // POST: /CatalogAdmin/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product, FormCollection collection)
        {
            try
            {
                if (product.ID == 0)
                {
                    product.CreatedAt = DateTime.Now;
                    _productService.AddProduct(product);
                    return RedirectToAction("Edit", new { id = product.ID });
                }

                _productService.EditProduct(product);

                var keys = collection.AllKeys.Where(q => q.StartsWith("param"));
                var result =
                    keys.Select(key => new Parameter {Value = collection[key], ID = int.Parse(key.Split('_')[1])})
                        .ToList();
                // TODO: Add update logic here
                _productService.SaveProductParameters(product.ID, result);

                var images = collection["images[]"];
                _productService.SaveManyImages(product.ID, images);

                return RedirectToAction("Edit", new {id = product.ID});
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Deactive(long id)
        {
            try
            {
                var product = _productService.GetProduct(id);
                if (product == null) return RedirectToAction("Index");

                product.IsActive = !product.IsActive;
                _productService.Save();

                return RedirectToAction("Index", new {id = product.CategoryID});
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult IsPopular(long id)
        {
            try
            {
                var product = _productService.GetProduct(id);
                if (product == null) return RedirectToAction("Index");

                product.IsPopular = !product.IsPopular;
                _productService.Save();

                return RedirectToAction("Index", new { id = product.CategoryID });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetProduct(int categoryID)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                var products =
                    _productService.GetProducts(categoryID)
                        .Select(
                            q =>
                                new ProductAjaxResponse
                                {
                                    ID = q.ID,
                                    CategoryName = q.Category.Name,
                                    ManufacturerName = q.Manufacturer.Name,
                                    Title = q.Title,
                                    IsActive = q.IsActive,
                                    IsPopular = q.IsPopular
                                })
                        .ToList();
                return Json(products);
            }
            catch
            {
                return Json("error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetProductPhotos(int productID)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                var product =
                    _productService.GetProduct(productID);

                var result =
                    product.ProductImage.Select(
                        q =>
                            String.Format(
                                "<img src='/Images/Catalog/Product/{0}' class='file-preview-image' alt='The Moon' title='The Moon'>",
                                q.ImageName)).ToArray();

                return Json(result);
            }
            catch
            {
                return Json("error");
            }
        }

        private struct ProductAjaxResponse
        {
            public string CategoryName { get; set; }
            public string ManufacturerName { get; set; }
            public string Title { get; set; }
            public bool IsPopular { get; set; }
            public bool IsActive { get; set; }
            public long ID { get; set; }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetCategories(int groupId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var secondCategories = new List<object>();

            foreach (var s in _shopService.GetCategories(groupId))
            {
                secondCategories.Add(new {Id = s.ID, s.Name});
            }

            return Json(secondCategories);
        }
    }
    
}
