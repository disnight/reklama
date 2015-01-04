using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Catalogs;
using Domain.Repository.Admin;
using Domain.Repository.Shared;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.SpreadSheet;
using PagedList;
using Reklama.Attributes;
using Reklama.Data.Entities;
using Reklama.Data.Servises;
using Reklama.Filters;
using Reklama.Models;
using Reklama.Models.ViewModels.Catalog;
using Reklama.ViewModels.Catalog;
using Reklama.ViewModels.Shops;
using WebMatrix.WebData;
using Shop = Reklama.Data.Entities.Shop;

namespace Reklama.Controllers
{
    public class ShopsController : Controller
    {
        private readonly ShopsService _shopService = new ShopsService();
        private readonly ProductService _productService = new ProductService();
        private readonly IProfileRepository _profileRepository;
        private readonly IConfigRepository _configRepository;
        private readonly ICityRepository _cityRepository;

        public ShopsController(IProfileRepository profileRepository, IConfigRepository configRepository, ICityRepository cityRepository)
        {

            _profileRepository = profileRepository;
            _configRepository = configRepository;
            _cityRepository = cityRepository;

            var rc = new ReklamaContext();
            _configRepository.Context = _cityRepository.Context = rc;
        }

        public ActionResult Details(int id, int? commentPage)
        {
            var shop = _shopService.GetShop(id);
            if (shop == null)
            {
                HttpNotFound();
            }
            _profileRepository.Context = new ReklamaContext();
            var result = new ShopDetailsPageViewModel(_profileRepository) { Shop = shop };

            if (shop.ShopFeedback.Any())
            {
                result.Comments = shop.ShopFeedback.Select(q => new FeedbackViewModel(_profileRepository)
                {
                    Comment = q.Comment,
                    ID = q.ID,
                    UserID = q.UserID,
                    CreatedAt = q.CreatedAt
                }).ToPagedList(commentPage ?? 1,
                    ProjectConfiguration.Get.CommentsOnPage);
            }

            return View(result);
        }

        public ActionResult CategoriesView(int shopID)
        {
            return PartialView("_Categories", new CategoriesViewModel
            {
                Groups = _shopService.GetShopCategories(shopID),
                ShopID = shopID
            });
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult FeedbackDelete(int commentId)
        {
            var comment = _shopService.GetShopFeedback(commentId);

            if (comment == null)
            {
                return HttpNotFound();
            }
            try
            {
                _shopService.ShopFeedbackDelete(commentId);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }
            return RedirectToAction("Details", "Shops", new { id = comment.ShopID });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult CreateShopFeedback(ShopCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _shopService.ShopFeedbackCreate(new Data.Entities.ShopFeedback
                    {
                        Comment = commentViewModel.Comment,
                        ShopID = commentViewModel.ShopId,
                        CreatedAt = DateTime.Now,
                        UserID = WebSecurity.CurrentUserId
                    });
                }
                catch (DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return RedirectToAction("Details", "Shops", new { id = commentViewModel.ShopId });
            }
            else
            {
                TempData["Comment"] = commentViewModel.Comment;
                return RedirectToAction("Details", "Shops", new { id = commentViewModel.ShopId });
            }
        }

        public ActionResult ProductsShop(int shopID, int categoryID, ShopProductsFilter filter = null)
        {
            if (filter == null)
            {
                filter = new ShopProductsFilter { CategoryID = categoryID, ShopID = shopID };
            }
            var result = new ShopProductsViewModel();

            var shopProducts = _shopService.GetShopProducts(filter.ShopID, filter.CategoryID).Sort(filter.SortOrder, filter.SortOptions, false);

            result.Category = _productService.GetCategory(categoryID);
            result.Products = shopProducts.Select(q => q.Product).ToPagedList(filter.Page, filter.PageSize);
            result.Filter = filter;

            return View(result);
        }
        public ActionResult ViewShopPage(int id)
        {
            var shop = _shopService.GetShop(id);
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            var result = new ShopPageViewModel() { Shop = shop };

            var categories = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories.Any())
                result.MonthlyFee = categories.Sum(x => x.Price);

            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            return View(result);
        }

        [Authorize]
        public ActionResult EditShopPage(int id)
        {
            var shop = _shopService.GetShop(id);
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }

            _profileRepository.Context = new ReklamaContext();
            var result = new ShopPageViewModel() { Shop = shop };

            var categories = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories.Any())
                result.MonthlyFee = categories.Sum(x => x.Price);

            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            return View(result);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditShopPage(ShopPageViewModel model)
        {
            var shop = _shopService.GetShop(model.Shop.ID);

            if (WebSecurity.CurrentUserId == shop.UserID || User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        shop.Phone = model.Shop.Phone;
                        shop.Site = model.Shop.Site;
                        shop.Icq = model.Shop.Icq;
                        shop.Skype = model.Shop.Skype;
                        shop.Description = model.Shop.Description;
                        shop.Monday = model.Shop.Monday;
                        shop.Tuesday = model.Shop.Tuesday;
                        shop.Wednesday = model.Shop.Wednesday;
                        shop.Thursday = model.Shop.Tuesday;
                        shop.Friday = model.Shop.Friday;
                        shop.Saturday = model.Shop.Saturday;
                        shop.Sunday = model.Shop.Sunday;
                        if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
                        {
                            shop.Title = model.Shop.Title;
                        }
                        _shopService.Save();
                        return RedirectToAction("ViewShopPage", "Shops", new { id = shop.ID });
                    }
                    catch
                    {
                        TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                        return View(model);
                    }
                }
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                return View(model);
            }
            else
            {
                return RedirectToRoute("RestrictedAccess");
            }
        }



        [Authorize]
        public ActionResult RegistrationData(int id)
        {
            var shop = _shopService.GetShop(id);

            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var result = new ShopRegistrationDataViewModel();

            var categories = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories.Any())
                result.MonthlyFee = categories.Sum(x => x.Price);
            result.Shop = shop;
            result.ChangeRegistrationDataHelp = _configRepository.ReadByName("ChangeRegistrationDataHelp").Value;

            return View(result);
        }


        [Authorize]
        public ActionResult RegistrationDataEdit(int id)
        {
            var shop = _shopService.GetShop(id);

            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var result = new ShopRegistrationDataViewModel();

            var categories = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories.Any())
                result.MonthlyFee = categories.Sum(x => x.Price);
            result.Shop = shop;
            result.ChangeRegistrationDataHelp = _configRepository.ReadByName("ChangeRegistrationDataHelp").Value;

            return View(result);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RegistrationDataEdit(ShopRegistrationDataViewModel model)
        {
            var shop = _shopService.GetShop(model.Shop.ID);

            if (WebSecurity.CurrentUserId == shop.UserID || User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        shop.Phone = model.Shop.Phone;
                        shop.CityID = model.Shop.CityID;
                        shop.Requisites = model.Shop.Requisites;
                        if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
                        {
                            shop.Title = model.Shop.Title;
                            shop.CompanyName = model.Shop.CompanyName;
                        }
                        _shopService.Save();
                        return RedirectToAction("RegistrationData", "Shops", new { id = shop.ID });
                    }
                    catch
                    {
                        TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                        return View(model);
                    }
                }
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                return View(model);
            }
            else
            {
                return RedirectToRoute("RestrictedAccess");
            }
        }

        [HttpGet]
        public void ExportProduct(int categoryId, int shopID, ImportType type)
        {
            if(categoryId == 0)return;
            var category = _shopService.GetCategory(categoryId);

            string fileName = "";
            IEnumerable<ExportProductForShop> products = null;
            switch (type)
            {
                    case ImportType.All:
                    fileName = "all";
                    products = _productService.GetProductsForExportByAll(shopID);
                    break;
                case ImportType.Binded:
                    fileName = "all_binded";
                    products = _productService.GetProductsForExportByBinded(shopID);
                    break;
                case ImportType.Category:
                    fileName = category.Name.Replace(" ", "_").Replace("-", "_");
                    products = _productService.GetProductsForExportByCategory(categoryId, shopID);
                    break;
            }

            var workbook = new Workbook();
            var worksheet = new Worksheet("First Sheet");
            for (var k = 0; k < 200; k++)
            {
                worksheet.Cells[k, 0] = new Cell(null);
            }

            worksheet.Cells[0, 0] = new Cell("Category");
            worksheet.Cells[0, 1] = new Cell("Manufacturer");
            worksheet.Cells[0, 2] = new Cell("Product");
            worksheet.Cells[0, 3] = new Cell("Is in stock");
            worksheet.Cells[0, 4] = new Cell("Price");
            worksheet.Cells[0, 8] = new Cell("Доступные статусы товара");

            var statuses = _productService.GetShopProductStatus();
            var z = 1;
            foreach (var status in statuses)
            {
                worksheet.Cells[z, 8] = new Cell(status.Name);
                z++;
            }

            if (products.Any())
            {
                var i = 1;
                foreach (var product in products)
                {
                    worksheet.Cells[i, 0] = new Cell(product.Product.Category.Name);
                    worksheet.Cells[i, 1] = new Cell(product.Product.Manufacturer.Name);
                    worksheet.Cells[i, 2] = new Cell(product.Product.Title);
                    if (product.ShopProduct != null)
                    {
                        worksheet.Cells[i, 3] = new Cell(product.ShopProduct.ShopProductStatus.Name);
                        worksheet.Cells[i, 4] = new Cell(product.ShopProduct.Price);
                    }
                    i++;
                }
            }
                
            worksheet.Cells.ColumnWidth[0] = 7000;
            worksheet.Cells.ColumnWidth[1] = 5000;
            worksheet.Cells.ColumnWidth[2] = 10000;
            worksheet.Cells.ColumnWidth[3] = 4000;
            worksheet.Cells.ColumnWidth[8] = 7000;
            workbook.Worksheets.Add(worksheet);

            var stream = new MemoryStream();
            workbook.SaveToStream(stream);
            stream.Position = 0;
            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition",
                String.Format("attachment; filename={0}_products.xls", fileName));
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        [HttpPost]
        public ActionResult ImportShopProducts(int shopID, FormCollection collection)
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
            }

            var workbook = Workbook.Load(file.InputStream);
            var worksheet = workbook.Worksheets[0];

            var rowsIndexs = worksheet.Cells.Rows.Where(q => q.Key > 0).Select(q => q.Key).OrderBy(q => q);

            var result = new List<ShopProduct>();

            foreach (var rowIndex in rowsIndexs)
            {
                var sShopProductStatus = worksheet.Cells[rowIndex, 3].StringValue;
                var isEmptyStatus = String.IsNullOrWhiteSpace(sShopProductStatus);

                var sPrice = worksheet.Cells[rowIndex, 4].StringValue;
                var isEmptyPrice = String.IsNullOrWhiteSpace(sPrice);

                var sProduct = worksheet.Cells[rowIndex, 2].StringValue;

                if(isEmptyPrice && isEmptyStatus) continue;

                if (isEmptyPrice)
                {
                    actionResult.Add(new ImportResultItem
                    {
                        ResultType = ImportResultType.Errror,
                        Message = "Для привязки товара необходимо указать 'Is in stock' и 'Price'. Вы не указали 'Price' для <strong>" + sProduct + "</strong>"
                    });
                    continue;
                }

                if (isEmptyStatus)
                {
                    actionResult.Add(new ImportResultItem
                    {
                        ResultType = ImportResultType.Errror,
                        Message = "Для привязки товара необходимо указать 'Is in stock' и 'Price'. Вы не указали 'Is in stock' для <strong>" + sProduct + "</strong>"
                    });
                    continue;
                }

                var shopProductStatus = _productService.GetShopProductStatusByName(sShopProductStatus);
                if (shopProductStatus == null)
                {
                    actionResult.Add(new ImportResultItem
                    {
                        ResultType = ImportResultType.Errror,
                        Message = "Статус товара с именем <strong>" + sShopProductStatus + "</strong> не существует."
                    });
                    continue;
                }
                
                decimal price;
                if (!decimal.TryParse(sPrice, out price))
                {
                    actionResult.Add(new ImportResultItem
                    {
                        ResultType = ImportResultType.Errror,
                        Message = "Указана неверная цена для <strong>" + sProduct + "</strong>"
                    });
                    continue;
                }

                
                var product = _productService.GetProductByName(sProduct);
                if (product == null)
                {
                    actionResult.Add(new ImportResultItem
                    {
                        ResultType = ImportResultType.Errror,
                        Message = "Товар с именем <strong>" + sProduct + "</strong> не существует. Название товара менять нельзя!"
                    });
                    continue;
                }

                
                var shopProduct = new ShopProduct();
                shopProduct.ShopID = shopID;
                shopProduct.ProductID = product.ID;
                shopProduct.ShopProductStatusID = shopProductStatus.ID;
                shopProduct.Price = price;

                result.Add(shopProduct);
              
            }

            actionResult.Add(_shopService.ImportShopProduct(result));
            TempData["actionResult"] = actionResult;
            return RedirectToAction("ShopProducts", new { Id = shopID });
        }

        [HttpGet]
        public ActionResult BaseProducts(ProductForShopViewModel model, FilterParams filter = null)
        {
            if (filter == null)
                filter = new FilterParams();

            var shop = _shopService.GetShop(filter.Id);
            
            //Verification access
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var groups = _shopService.GetGroups().ToList();
            var selectedGroup = filter.CategoryId == 0
                ? groups.First()
                : groups.First(q => q.ID == filter.CategoryId);

            var categories = _shopService.GetCategories(selectedGroup.ID);
            var selectedCategory = filter.SecondCategoryId == 0
                ? categories.First()
                : categories.First(q => q.ID == filter.SecondCategoryId);

            var emptyManufacturer = new Manufacturer { ID = 0, Name = "Все" };
            var manufacturers = new List<Manufacturer> { emptyManufacturer };
            manufacturers.AddRange(_shopService.GetManufacturers(selectedCategory.ID));
            Manufacturer selectedManufacturer = emptyManufacturer;
            if (manufacturers.Count > 1)
            {
                selectedManufacturer = filter.ThirdCategoryId == 0
                ? manufacturers.First()
                : manufacturers.First(q => q.ID == filter.ThirdCategoryId);
            }

            var shopProducts = _shopService.GetShopProducts(shop.ID);

            model.Filter = filter;
            model.Groups = new SelectList(groups, "ID", "Name", selectedGroup);
            model.Categories = new SelectList(categories, "ID", "Name", selectedCategory);
            model.Manufacturers = new SelectList(manufacturers, "ID", "Name", selectedManufacturer);
            model.CurrentCategoryID = selectedCategory.ID;
            model.Shop = shop;
            model.MonthlyFee = 0;
            model.ShopProducts = shopProducts;
            model.Products = selectedCategory.Product.Where(q => selectedManufacturer.ID == 0 || selectedManufacturer.ID == q.ManufacturerID).ToPagedList(filter.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);

            var categories2 = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories2.Any())
                model.MonthlyFee = categories2.Sum(x => x.Price);

            return View(model);
        }

        public ActionResult ShopProducts(ProductForShopViewModel model, FilterParams filter = null, List<ImportResultItem> importResult = null )
        {
            if (filter == null)
                filter = new FilterParams();

            if (TempData.ContainsKey("actionResult"))
            {
                model.ImportResult = (List<ImportResultItem>)TempData["actionResult"];
                //TempData.Remove("actionResult");
            }
            

            var shop = _shopService.GetShop(filter.Id);

            //Verification access
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var source = _shopService.GetShopCategories(shop.ID).ToList();
            var groups = new List<Group>();

            if (source.Any())
            {
                groups.AddRange(source.Select(q => q.Key));
                var selectedGroup = filter.CategoryId == 0
                    ? groups.First()
                    : groups.First(q => q.ID == filter.CategoryId);

                var categories = new List<Category>();
                categories.AddRange(source.FirstOrDefault(q => q.Key.ID == selectedGroup.ID).Distinct());
                var selectedCategory = filter.SecondCategoryId == 0
                    ? categories.First()
                    : categories.First(q => q.ID == filter.SecondCategoryId);

                var emptyManufacturer = new Manufacturer { ID = 0, Name = "Все" };
                var manufacturers = new List<Manufacturer> { emptyManufacturer };
                manufacturers.AddRange(_shopService.GetCategoryManufacturers(shop.ID, selectedCategory.ID));
                Manufacturer selectedManufacturer = emptyManufacturer;
                if (manufacturers.Count > 1)
                {
                    selectedManufacturer = filter.ThirdCategoryId == 0
                    ? manufacturers.First()
                    : manufacturers.First(q => q.ID == filter.ThirdCategoryId);
                }

                var shopProducts = _shopService.GetShopProducts(shop.ID).ToList();

                model.Filter = filter;
                model.Groups = new SelectList(groups, "ID", "Name", selectedGroup);
                model.Categories = new SelectList(categories, "ID", "Name", selectedCategory);
                model.Manufacturers = new SelectList(manufacturers, "ID", "Name", selectedManufacturer);
                model.ShopProducts = shopProducts;
                model.ShopProductsFiltered = shopProducts.Where(q => q.Product.CategoryID == selectedCategory.ID).Where(q => selectedManufacturer.ID == 0 || selectedManufacturer.ID == q.Product.ManufacturerID).ToPagedList(filter.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);

            }

            model.Shop = shop;
            model.MonthlyFee = 0;

            var categories2 = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories2.Any())
                model.MonthlyFee = categories2.Sum(x => x.Price);

            return View(model);
        }

        [Authorize(Roles = "Shop")]
        public ActionResult RedirectToBaseProducts()
        {
            var userId = WebSecurity.CurrentUserId;
            var shop = _shopService.GetShopByUserID(userId);
            return RedirectToAction("BaseProducts", new { id = shop.ID });
        }

        [Authorize]
        public ActionResult Create()
        {
            if (_shopService.IsExistShopByCurrentUser(WebSecurity.CurrentUserId))
            {
                TempData["error"] = "У вас уже создан магазин!";
                return Redirect("/");
            }
            ViewBag.Cities = _cityRepository.Read();
            return View();
        }

        //
        // POST: /Shop/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Shop shop)
        {
            if (ModelState.IsValid)
            {
                shop.UserID = WebSecurity.CurrentUserId;
                try
                {
                    _shopService.Create(shop);
                    TempData["notice"] = "Магазин создан, личная панель будет доступна после активации магазина администратором.";
                    return Redirect("/");
                }
                catch
                {
                    TempData["Error"] = "Произошла ошибка при создании магазина";
                    ViewBag.Cities = _cityRepository.Read();
                    return View(shop);
                }
            }
            return View(shop);
        }

        #region AJAX Methods

        [HttpPost]
        [Authorize]
        public JsonResult UploadShopLogo(string logo, string id)
        {
            var file = Request.Files.Get("myfile");
            var uploader = new ImageUploader(file);
            uploader.Quality = CompositingQuality.HighQuality;
            uploader.Interpolation = InterpolationMode.HighQualityBilinear;
            var shopId = Convert.ToInt32(id);

            var shop = _shopService.GetShop(shopId);

            try
            {
                if (shop == null || (!User.IsInRole("Administrator") && !User.IsInRole("Moderator") && shop.UserID != WebSecurity.CurrentUserId))
                {
                    throw new Exception();
                }
                shop.Logo = uploader.UniqueName;
                _shopService.Save();
                uploader.Convert(ProjectConfiguration.Get.ShopLogoWidth, ProjectConfiguration.Get.ShopLogoHeight);
                uploader.Save("shopLogo");
            }
            catch (Exception)
            {
                return Json(new { status = "fail" }, "text/html");
            }

            var result = new
            {
                newFilename = uploader.UniqueName,
                status = "success"
            };

            //if (logo != "/Images/System/no_logo.png")
            //{
            //    RemoveRealtyOnlyImage(logo);
            //}
            return Json(result, "text/html");
        }

        [HttpPost]
        [Authorize]
        public JsonResult RemoveShopLogo(string shopId, string image)
        {
            var id = Convert.ToInt32(shopId);

            try
            {
                var shop = _shopService.GetShop(id);

                if (shop == null)
                {
                    throw new Exception();
                }

                if (shop.UserID != WebSecurity.CurrentUserId && !User.IsInRole("Administrator"))
                {
                    throw new Exception();
                }

                shop.Logo = null;
                _shopService.Save();
                var path = AppDomain.CurrentDomain.BaseDirectory + image;
                System.IO.File.Delete(path);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetCategories(int shopId, int groupId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var secondCategories = new List<object>();

            foreach (var s in _shopService.GetCategories(groupId))
            {
                secondCategories.Add(new { Id = s.ID, s.Name });
            }

            return Json(secondCategories);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetShopCategories(int shopId, int groupId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var categories = _shopService.GetShopCategories(shopId, groupId).FirstOrDefault();
            if (categories == null)
            {
                throw new InvalidCastException("Not an correct group");
            }

            var secondCategories = new List<object>();

            foreach (var s in categories)
            {
                secondCategories.Add(new { Id = s.ID, s.Name });
            }

            return Json(secondCategories);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetManufacturers(int categoryId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var result = new List<object>();
            var manufacturers = _shopService.GetManufacturers(categoryId).ToList();
            if (manufacturers.Any())
            {
                result.Add(new { Id = 0, Name = "Все" });
            }
            foreach (var s in manufacturers)
            {
                result.Add(new { Id = s.ID, s.Name });
            }

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetShopManufacturers(int shopId, int categoryId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var result = new List<object>();
            var manufacturers = _shopService.GetCategoryManufacturers(shopId, categoryId).ToList();
            if (manufacturers.Any())
            {
                result.Add(new { Id = 0, Name = "Все" });
            }
            foreach (var s in manufacturers)
            {
                result.Add(new { Id = s.ID, s.Name });
            }

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult AddProductToShop(int productId, int shopId, decimal price)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                _shopService.AddProductToShop(new ShopProduct() { ProductID = productId, ShopID = shopId, Price = price, ShopProductStatusID = 1 });
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult UpdateShopProduct(int shopId, int productId, decimal price)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var shop = _shopService.GetShop(shopId);
            if (shop == null)
            {
                return Json(new { status = "fail" }, "text/html");
            }


            if (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                return Json(new { status = "fail" }, "text/html");
            }

            try
            {
                _shopService.ShopProductsUpdate(shopId, productId, price);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult RemoveProductFromShop(int productId, int shopId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                _shopService.RemoveProductFromShop(productId, shopId);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        #endregion
    }

    public enum ImportType
    {
        Category,
        Binded,
        All
    }
}
