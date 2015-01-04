using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Domain.Repository.Shared;
using Reklama.Data.Base;
using Reklama.Data.Entities;

namespace Reklama.Data.Servises
{
    public class ShopsService
    {
        private readonly Base.IRepository<Shop> _shopRepository;
        private readonly Base.IRepository<ShopProduct> _shopProductRepository;
        private readonly Base.IRepository<ShopFeedback> _shopFeedbackRepository;
        private readonly Base.IRepository<Category> _categoriesRepository;
        private readonly Base.IRepository<CategoryParametersSection> _categoryParametersSectionRepository;
        private readonly Base.IRepository<CategoryParameter> _categoryParameterRepository;
        private readonly Base.IRepository<Product> _productssRepository;
        private readonly Base.IRepository<Group> _groupsRepository;
        private readonly Base.IRepository<Manufacturer> _manufacturersRepository;
        
        public ShopsService()
        {
            var context = new ReklamaCustomEntities();
            _shopRepository = new Repository<Shop>(context);
            _shopProductRepository = new Repository<ShopProduct>(context);
            _shopFeedbackRepository = new Repository<ShopFeedback>(context);
            _categoriesRepository = new Repository<Category>(context);
            _categoryParametersSectionRepository = new Repository<CategoryParametersSection>(context);
            _categoryParameterRepository = new Repository<CategoryParameter>(context);
            _productssRepository = new Repository<Product>(context);
            _groupsRepository = new Repository<Group>(context);
            _manufacturersRepository = new Repository<Manufacturer>(context);
        }

        public Shop GetShop(int shopID)
        {
            return _shopRepository.FirstOrDefault(q => q.ID == shopID);
        }

        public IEnumerable<Shop> GetShops()
        {
            return _shopRepository.GetAll();
        }

        public Shop GetShopByUserID(int userID)
        {
            return _shopRepository.FirstOrDefault(q => q.UserID == userID);
        }

        public bool IsExistShopByCurrentUser(int userID)
        {
            return _shopRepository.Find(q => q.UserID == userID).Any();
        }

        public void Create(Shop shop)
        {
            _shopRepository.Add(shop);
            _shopRepository.SaveChanges();
        }

        public void Save()
        {
            _shopRepository.SaveChanges();
        }

        public IEnumerable<IGrouping<Group, Category>> GetShopCategories(int shopID)
        {
            return _shopProductRepository.Find(q => q.ShopID == shopID).Select(q => q.Product.Category).GroupBy(q => q.Group);
        }

        public IEnumerable<IGrouping<Group, Category>> GetShopCategories(int shopID, int groupID)
        {
            var result =
                _shopProductRepository.Find(q => q.ShopID == shopID)
                    .Select(q => q.Product.Category)
                    .Where(q => q.GroupID == groupID)
                    .ToList();
            return result.Distinct().GroupBy(q => q.Group);
        }

        public IEnumerable<Group> GetGroups()
        {
            return _groupsRepository.GetAll().Where(q => !q.IsDeleted);
        }

        public Group GetGroup(int id)
        {
            return _groupsRepository.FirstOrDefault(q => q.ID == id);
        }

        public bool AddGroups(Group group)
        {
            try
            {
                _groupsRepository.Add(group);
                _groupsRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                _manufacturersRepository.Add(manufacturer);
                _manufacturersRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<Category> GetCategories(int groupID)
        {
            return _categoriesRepository.Find(q => q.GroupID == groupID);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoriesRepository.GetAll();
        }

        public IEnumerable<Category> GetNewCategories()
        {
            return _categoriesRepository.Find(q => q.IsActive && q.IsNew);
        }

        public IEnumerable<Category> GetPopularCategories()
        {
            return _categoriesRepository.Find(q => q.IsActive && q.IsPopular);
        }

        public IEnumerable<Product> GetPopularProducts()
        {
            return _productssRepository.Find(q => q.IsActive && q.IsPopular);
        }

        public Category GetCategory(int categoryID)
        {
            return _categoriesRepository.FirstOrDefault(q => q.ID == categoryID);
        }

        public bool AddCategory(Category category)
        {
            try
            {
                _categoriesRepository.Add(category);
                _categoriesRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Parameters Section
        public CategoryParametersSection GetSection(int sectionID)
        {
            try
            {
                return _categoryParametersSectionRepository.FirstOrDefault(q => q.ID == sectionID);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddSection(CategoryParametersSection section)
        {
            try
            {
                _categoryParametersSectionRepository.Add(section);
                _categoryParametersSectionRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSection(CategoryParametersSection section)
        {
            try
            {
                _categoryParametersSectionRepository.Edit(section);
                _categoryParametersSectionRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveSection(CategoryParametersSection section)
        {
            try
            {
                _categoryParametersSectionRepository.Delete(section);
                _categoryParametersSectionRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region parameters
        public CategoryParameter GetParametr(int paramID)
        {
            try
            {
                return _categoryParameterRepository.FirstOrDefault(q => q.ID == paramID);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddParametr(CategoryParameter param)
        {
            try
            {
                _categoryParameterRepository.Add(param);
                _categoryParameterRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateParametr(CategoryParameter param)
        {
            try
            {
                _categoryParameterRepository.Edit(param);
                _categoryParameterRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveParametr(CategoryParameter param)
        {
            try
            {
                _categoryParameterRepository.Delete(param);
                _categoryParameterRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        public bool UpdateCategory(Category category)
        {
            try
            {
                _categoriesRepository.Edit(category);
                _categoriesRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Manufacturer GetManufacturer(int manufacturerID)
        {
            return _manufacturersRepository.FirstOrDefault(q => q.ID == manufacturerID);
        }

        public void RemoveManufacturer(Manufacturer manufacturer)
        {
            _manufacturersRepository.Delete(manufacturer);
        }

        public IEnumerable<Manufacturer> GetManufacturers(int categoryID)
        {
            var result = _productssRepository.Find(q => q.CategoryID == categoryID).Select(q => q.Manufacturer).ToList();
            return result.Distinct();
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            var result = _manufacturersRepository.GetAll().ToList();
            return result.Distinct();
        }

        public IEnumerable<ShopProduct> GetShopProducts(int shopID)
        {
            return _shopProductRepository.Find(q => q.ShopID == shopID);
        }

        public bool ShopProductsUpdate(int shopID, int productID, decimal price)
        {
            var sp = _shopProductRepository.FirstOrDefault(q => q.ShopID == shopID && q.ProductID == productID);
            if (sp == null) return false;

            sp.Price = price;
            _shopProductRepository.SaveChanges();
            return true;
        }

        public IEnumerable<Manufacturer> GetCategoryManufacturers(int shopID, int categoryID)
        {
            var result = _shopProductRepository.Find(q => q.ShopID == shopID && q.Product.CategoryID == categoryID).Select(q => q.Product.Manufacturer);
            return result.Distinct();
        }

        public IEnumerable<ShopProduct> GetShopProductsByManufacturer(int shopID, int manufacturerID)
        {
            return _shopProductRepository.Find(q => q.ShopID == shopID && (q.Product.ManufacturerID == manufacturerID || manufacturerID == 0));
        }
        public IEnumerable<ShopProduct> GetShopProducts(int shopID, int categoryID)
        {
            return _shopProductRepository.Find(q => q.ShopID == shopID && q.Product.CategoryID == categoryID);
        }

        public ShopFeedback GetShopFeedback(int feedbackID)
        {
            return _shopFeedbackRepository.FirstOrDefault(q => q.ID == feedbackID);
        }

        public bool ShopFeedbackDelete(int id)
        {
            try
            {
                _shopFeedbackRepository.Delete(q => q.ID == id);
                _shopFeedbackRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool AddProductToShop(ShopProduct shopProduct)
        {
            try
            {
                _shopProductRepository.Add(shopProduct);
                _shopFeedbackRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool RemoveProductFromShop(int productID, int shopID)
        {
            try
            {
                _shopProductRepository.Delete(q => q.ShopID == shopID && q.ProductID == productID);
                _shopFeedbackRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ShopFeedbackCreate(ShopFeedback feedback)
        {
            try
            {
                _shopFeedbackRepository.Add(feedback);
                _shopFeedbackRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public ImportResultItem ImportShopProduct(IEnumerable<ShopProduct> source)
        {
            var success = 0;
            var error = 0;
            var added = 0;
            var updated = 0;

            foreach (var item in source)
            {
                try
                {
                    var isAdd = true;
                    var dbItem =
                    _shopProductRepository.FirstOrDefault(q => q.ShopID == item.ShopID && q.ProductID == item.ProductID);
                    if (dbItem == null)
                    {
                        _shopProductRepository.Add(item);
                    }
                    else
                    {
                        dbItem.ShopProductStatusID = item.ShopProductStatusID;
                        dbItem.Price = item.Price;
                        isAdd = false;
                    }

                    _shopProductRepository.SaveChanges();
                    if (isAdd)
                    {
                        added ++;}
                    else
                    {
                        updated++;}
                    success++;
                }
                catch (Exception ex)
                {
                    error++;
                }
            }

            return new ImportResultItem
            {
                Message = String.Format("Привязка продуктов завершена. Всего обработано {0} товаров: из которых {1} добавлено, {2} обнавлено, {3} обработано с ошибкой.", success, added, updated, error),
                ResultType = ImportResultType.CommonError
            };
        }

    }
}