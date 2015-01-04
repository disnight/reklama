using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Enums;
using Reklama.Data.Base;
using Reklama.Data.Entities;
using Reklama.Data.Models;
using WebGrease.Css.Extensions;

namespace Reklama.Data.Servises
{

    public class ProductService
    {
        private readonly ReklamaCustomEntities _context;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductFeedback> _productFeedbacksRepository;
        private readonly IRepository<ProductParameterValue> _productParameterValueRepository;
        private readonly IRepository<ProductImage> _productImageRepository;
        private readonly IRepository<ShopProductStatus> _shopProductStatusRepository;
        public ProductService()
        {
            _context = new ReklamaCustomEntities();
            _groupRepository = new Repository<Group>(_context);
            _productRepository = new Repository<Product>(_context);
            _categoryRepository = new Repository<Category>(_context);
            _productFeedbacksRepository = new Repository<ProductFeedback>(_context);
            _productParameterValueRepository = new Repository<ProductParameterValue>(_context);
            _productImageRepository = new Repository<ProductImage>(_context);
            _shopProductStatusRepository = new Repository<ShopProductStatus>(_context);
        }

        public static string ProductImagesPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "Images/Catalog/Product/";
            }
        }

        public static string ThumbProductImagesPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "Images/Thumbnails/Product";
            }
        }


        public IEnumerable<Group> GetAllGroups()
        {
            return _groupRepository.GetAll().Where(q => !q.IsDeleted);
        }

        public IEnumerable<Product> GetProducts(int categoryID)
        {
            return _productRepository.Find(q => categoryID == 0 || q.CategoryID == categoryID);
        }

        public IEnumerable<ExportProductForShop> GetProductsForExportByCategory(int categoryID, int shopID)
        {
            var result = (from product in _context.Product where product.CategoryID == categoryID
                join shop in _context.ShopProduct on product.ID equals shop.ProductID into sp
                          from subshop in sp.Where(w => w.ShopID == shopID).DefaultIfEmpty()
                select new ExportProductForShop {Product = product, ShopProduct = subshop});
            return result;
        }
        public IEnumerable<ExportProductForShop> GetProductsForExportByBinded(int shopID)
        {
            var result = (from product in _context.Product
                          join shop in _context.ShopProduct on product.ID equals shop.ProductID
                          where shop.ShopID == shopID
                          orderby product.Category.Name
                          select new ExportProductForShop { Product = product, ShopProduct = shop });
            return result;
        }
        public IEnumerable<ExportProductForShop> GetProductsForExportByAll(int shopID)
        {
            var result = (from product in _context.Product
                          join shop in _context.ShopProduct on product.ID equals shop.ProductID into sp
                          from subshop in sp.Where(w => w.ShopID == shopID).DefaultIfEmpty()
                          orderby product.Category.Name
                          select new ExportProductForShop { Product = product, ShopProduct = subshop });
            return result;
        }

        public Category GetCategory(int categoryID)
        {
            return _categoryRepository.Find(q => q.ID == categoryID).FirstOrDefault();
        }

        public Product GetProduct(long ID)
        {
            return _productRepository.Find(q => q.ID == ID).FirstOrDefault();
        }

        public Product GetProductByName(string name)
        {
            return _productRepository.Find(q => q.Title.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
        }

        public ShopProductStatus GetShopProductStatusByName(string name)
        {
            return _shopProductStatusRepository.Find(q => q.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
        }

        public IEnumerable<ShopProductStatus> GetShopProductStatus()
        {
            return _shopProductStatusRepository.GetAll().OrderBy(q => q.Name);
        }

        public bool AddProduct(Product product)
        {
            try
            {
                _productRepository.Add(product);
                _productRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditProduct(Product product)
        {
            try
            {
                _productRepository.Edit(product);
                _productRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ImportResultItem> ImportProducts(List<LocalProduct> data)
        {
            var result = new List<ImportResultItem>();

            foreach (var product in data)
            {
                var currentResult = new ImportResultItem();

                if (String.IsNullOrWhiteSpace(product.Product.Title))
                {
                    currentResult.ResultType = ImportResultType.Errror;
                    currentResult.Message = "Product title cannot be empty. Please fill the <strong>Title</strong> field";
                    result.Add(currentResult);
                    continue;
                }

                if (String.IsNullOrWhiteSpace(product.Product.SmallDescription))
                {
                    currentResult.ResultType = ImportResultType.Errror;
                    currentResult.Message = "Small Description cannot be empty. " + String.Format("Product named <strong>{0}</strong> didn't import.",product.Product.Title) + " Please fill the <strong>Small Description</strong> field";
                    result.Add(currentResult);
                    continue;
                }

                if (String.IsNullOrWhiteSpace(product.Product.Description))
                {
                    currentResult.ResultType = ImportResultType.Errror;
                    currentResult.Message = "Description cannot be empty. " + String.Format("Product named <strong>{0}</strong> didn't import.", product.Product.Title) + " Please fill the <strong>Description</strong> field";
                    result.Add(currentResult);
                    continue;
                }

                try
                {

                    if (product.Product.ID == 0)
                    {
                        if (_context.Product.Any(q => q.Title.ToLower().Trim() == product.Product.Title.ToLower().Trim()))
                        {
                            currentResult.ResultType = ImportResultType.Errror;
                            currentResult.Message = "Product named <strong>" + product.Product.Title + "</strong> already exists";
                            result.Add(currentResult);
                            continue;
                        }

                        product.Product.CreatedAt = DateTime.Now;
                        _context.Product.Add(product.Product);
                        _context.SaveChanges();

                        foreach (var param in product.ProductValues)
                        {
                            param.ProductID = product.Product.ID;
                            _context.ProductParameterValue.Add(param);
                        }

                        _context.SaveChanges();
                        currentResult.ResultType = ImportResultType.SuccessfullyAdded;
                        currentResult.Message = "Product named <strong>" + product.Product.Title + "</strong> was been successfully added";
                    }
                    else
                    {
                        var tempProject = _context.Product.FirstOrDefault(q => q.ID == product.Product.ID);
                        if (tempProject == null) continue;

                        tempProject.CategoryID = product.Product.CategoryID;
                        tempProject.ManufacturerID = product.Product.ManufacturerID;
                        tempProject.ReviewLink = product.Product.ReviewLink;
                        tempProject.Title = product.Product.Title;
                        tempProject.SmallDescription = product.Product.SmallDescription;
                        tempProject.Description = product.Product.Description;
                        tempProject.IsActive = product.Product.IsActive;

                        foreach (var param in product.ProductValues)
                        {
                            param.ProductID = product.Product.ID;
                            var dbValue =
                                _productParameterValueRepository.FirstOrDefault(
                                    q =>
                                        q.ProductID == param.ProductID &&
                                        q.CategoryParameterID == param.CategoryParameterID);
                            if (dbValue == null)
                            {
                                _context.ProductParameterValue.Add(new ProductParameterValue
                                {
                                    UnitID = param.UnitID,
                                    Value = param.Value,
                                    CategoryParameterID = param.CategoryParameterID,
                                    ProductID = param.ProductID
                                });
                                continue;
                            }

                            dbValue.UnitID = param.UnitID;
                            dbValue.Value = param.Value;
                        }
                        _context.SaveChanges();
                        currentResult.ResultType = ImportResultType.SuccessfullyUpdated;
                        currentResult.Message = "Product named <strong>" + product.Product.Title + "</strong> was been successfully updated";
                    }
                }
                catch (Exception ex)
                {
                    currentResult.ResultType = ImportResultType.Errror;
                    currentResult.Message =
                        String.Format(
                            "Product named <strong>{0}</strong> didn't import. Exception Message: {1}; InnerException Message: {2}",
                            product.Product.Title, 
                            ex.Message,
                            ex.InnerException != null ? ex.InnerException.Message : "");
                }
                result.Add(currentResult);
            }
            return result;
        }

        public IQueryable<IGrouping<CategoryParametersSection, CommonParameterObject>> GetProductParameters(int categoryID, long productID = 0)
        {
            var query = (from section in _context.CategoryParameter
                         join value in _context.ProductParameterValue on section.ID equals value.CategoryParameterID into gj
                         where section.CategoryParametersSection.CategoryID == categoryID
                         from subVal in
                             (from f in gj where f.ProductID == productID select f).DefaultIfEmpty()
                         select new CommonParameterObject { Section = section.CategoryParametersSection, Parameter = section, ParameterValue = subVal }).GroupBy(q => q.Section);
            return query;
        }

        public bool SaveProductParameters(long productID, List<Parameter> parameters)
        {
            try
            {
                var innerParameters =
                parameters.Where(q => !String.IsNullOrWhiteSpace(q.Value)).Select(
                    q => new ProductParameterValue { ProductID = productID, CategoryParameterID = q.ID, Value = q.Value }).ToList();

                var dbParameters = _productParameterValueRepository.Find(q => q.ProductID == productID);

                var parametersToAdd =
                    innerParameters.Except(dbParameters, new ProductValuesComparer()).ToList();

                var parametersToUpdate = innerParameters.Except(parametersToAdd).ToList();

                foreach (var parameter in parametersToAdd)
                {
                    _context.ProductParameterValue.Add(parameter);
                }

                foreach (var parameter in parametersToUpdate)
                {
                    var temp =
                        _context.ProductParameterValue.FirstOrDefault(
                            q =>
                                q.CategoryParameterID == parameter.CategoryParameterID &&
                                q.ProductID == parameter.ProductID);
                    if (temp == null) continue;

                    temp.Value = parameter.Value;
                }

                _productParameterValueRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void SaveManyImages(long productID, string imageNamesSeparated)
        {
            if (imageNamesSeparated != null)
            {
                var images = new Dictionary<string, bool>();
                imageNamesSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ForEach(q => images.Add(Path.GetFileName(q.Split(';')[0]), (q.Split(';').Length > 1) && q.Split(';')[1] == "true"));
                var issetImages = from i in _context.ProductImage where i.ProductID == productID select i.ImageName;
                var imagesSet = new HashSet<string>(issetImages);

                foreach (var image in images)
                {
                    var obj = new ProductImage
                    {
                        ProductID = productID,
                        IsTitular = image.Value,
                        IsDeleted = false,
                        ImageName = image.Key
                    };

                    InsertOrUpdateImage(obj);
                }

                var imagesToDeleteLink = imagesSet.Except(images.Select(q => q.Key)).ToArray();
                DeleteImages(productID, imagesToDeleteLink);
            }
        }

        public void DeleteImages(long productID, string[] images)
        {
            foreach (var image in images)
            {
                DeleteImage(productID, image);
            }
        }

        public void DeleteImage(long productID, string image)
        {
            var imageWithoutPath = image.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
            var announcementImage =
                _context.ProductImage.Where(a => a.ProductID == productID).First(
                    a => a.ImageName.Equals(imageWithoutPath));
            _productImageRepository.Delete(announcementImage);
            _productImageRepository.SaveChanges();

            var path = ProductImagesPath + imageWithoutPath;
            var thumbPath = ThumbProductImagesPath + imageWithoutPath;

            File.Delete(path);
            File.Delete(thumbPath);
        }

        public void InsertOrUpdateImage(ProductImage image)
        {
            var obj =
                _context.ProductImage.FirstOrDefault(
                    q => q.ProductID == image.ProductID && q.ImageName == image.ImageName);

            if (obj != null)
            {
                obj.IsTitular = image.IsTitular;
                _productImageRepository.Edit(obj);
            }
            else
            {
                _productImageRepository.Add(image);
            }
            _productImageRepository.SaveChanges();
        }

        public IEnumerable<Manufacturer> GetManufacturers(int categoryID)
        {
            return _productRepository.Find(q => q.CategoryID == categoryID).Select(q => q.Manufacturer).Distinct();
        }

        public bool ProductFeedbackCreate(ProductFeedback feedback)
        {
            try
            {
                _productFeedbacksRepository.Add(feedback);
                _productFeedbacksRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public ProductFeedback GetProductFeedback(long id)
        {
            try
            {
                return _productFeedbacksRepository.FirstOrDefault(q => q.ID == id);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public bool ProductFeedbackDelete(long id)
        {
            try
            {
                _productFeedbacksRepository.Delete(q => q.ID == id);
                _productFeedbacksRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IQueryable<IGrouping<CategoryParametersSection, ProductParameterValue>> GetParameters(long productId)
        {
            return _context.ProductParameterValue.Where(q => q.ProductID == productId)
                    .GroupBy(q => q.CategoryParameter.CategoryParametersSection);
        }

        private const int MinSearchWordLength = 3;

        public IEnumerable<Product> Search(string keywordPhrase, bool onlyByName)
        {
            IEnumerable<Product> resultSet = new List<Product>();

            var keywords = keywordPhrase.ToLowerInvariant().Trim()
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (keywords.Any())
            {
                //string baseWord = string.Empty;
                //foreach (string word in keywords)
                //{
                //    if (word.Length >= MinSearchWordLength)
                //    {
                //        resultSet = _productRepository.Find(e => e.Title.ToLower().Contains(word));
                //        baseWord = word;
                //        break;
                //    }
                //}


                // The resultSet is initialized. Filling it
                foreach (string keyword in keywords)
                {
                    if (keyword.Length < MinSearchWordLength) continue;

                    //if (!keyword.Equals(baseWord))
                    //{
                        var resultSetName = _productRepository.Find(e => e.Category.IsActive && e.IsActive && e.Title.ToLower().Contains(keyword));

                        if (resultSetName != null)
                        {
                            resultSet = resultSet.Union(resultSetName);
                        }
                    //}


                    // Searching by name, small description and description
                    if (!onlyByName)
                    {
                        var resultSetDescription = _productRepository.Find(e => e.Category.IsActive && e.IsActive && e.Description.ToLower().Contains(keyword));
                        var resultSetSmallDescription = _productRepository.Find(e => e.Category.IsActive && e.IsActive && e.SmallDescription.ToLower().Contains(keyword));

                        if (resultSetDescription != null)
                        {
                            resultSet = resultSet.Union(resultSetDescription);
                        }

                        if (resultSetSmallDescription != null)
                        {
                            resultSet = resultSet.Union(resultSetSmallDescription);
                        }
                    }
                }
            }

            return resultSet;
        }

        public bool Save()
        {
            try
            {
                _productRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }

    public class ProductValuesComparer : IEqualityComparer<ProductParameterValue>
    {
        public bool Equals(ProductParameterValue x, ProductParameterValue y)
        {
            return x.ProductID == y.ProductID && x.CategoryParameterID == y.CategoryParameterID;
        }

        public int GetHashCode(ProductParameterValue obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = obj.ProductID.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = obj.CategoryParameterID.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }
    }

    public class LocalProduct
    {
        public Product Product { get; set; }
        public List<ProductParameterValue> ProductValues = new List<ProductParameterValue>();
    }

    public class ExportProductForShop
    {
        public Product Product { get; set; }
        public ShopProduct ShopProduct { get; set; }
    }

    public class ImportResultItem
    {
        public ImportResultType ResultType { get; set; }
        public string Message { get; set; }
    }


    public enum ImportResultType
    {
        SuccessfullyAdded,
        SuccessfullyUpdated,
        Errror,
        Warning,
        CommonError
    }
}