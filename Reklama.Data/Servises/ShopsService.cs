using System;
using System.Collections.Generic;
using System.Linq;
using Reklama.Data.Base;
using Reklama.Data.Entities;

namespace Reklama.Data.Servises
{
    public class ShopsService
    {
        private readonly IRepository<Shop> _shopRepository;
        private readonly IRepository<ShopProduct> _shopProductRepository;
        private readonly IRepository<ShopFeedback> _shopFeedbackRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Manufacturer> _manufacturersRepository;
        private readonly IRepository<Product> _productssRepository;
        private readonly IRepository<Group> _groupsRepository;

        public ShopsService()
        {
            var context = new ReklamaCustomEntities();
            _shopRepository = new Repository<Shop>(context);
            _shopProductRepository = new Repository<ShopProduct>(context);
            _shopFeedbackRepository = new Repository<ShopFeedback>(context);
            _categoriesRepository = new Repository<Category>(context);
            _manufacturersRepository = new Repository<Manufacturer>(context);
            _productssRepository = new Repository<Product>(context);
            _groupsRepository = new Repository<Group>(context);
        }

        public Shop GetShop(int shopID)
        {
            return _shopRepository.FirstOrDefault(q => q.ID == shopID);
        }

        public Shop GetShopByUserID(int userID)
        {
            return _shopRepository.FirstOrDefault(q => q.UserID == userID);
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
            return _shopProductRepository.Find(q => q.ShopID == shopID).Select(q => q.Product.Category).Where(q => q.GroupID == groupID).GroupBy(q => q.Group);
        }

        public IEnumerable<Group> GetGroups()
        {
            return _groupsRepository.GetAll();
        }

        public IEnumerable<Category> GetCategories(int groupID)
        {
            return _categoriesRepository.Find(q => q.GroupID == groupID);
        }
        public IEnumerable<Manufacturer> GetManufacturers(int categoryID)
        {
            return _productssRepository.Find(q => q.CategoryID == categoryID).Select(q => q.Manufacturer).Distinct();
        }

        public IEnumerable<ShopProduct> GetShopProducts(int shopID)
        {
            return _shopProductRepository.Find(q => q.ShopID == shopID);
        }

        public bool ShopProductsUpdate(int shopID, int productID, decimal price)
        {
            var sp =  _shopProductRepository.FirstOrDefault(q => q.ShopID == shopID && q.ProductID == productID);
            if (sp == null) return false;

            sp.Price = price;
            _shopProductRepository.SaveChanges();
            return true;
        }

        public IEnumerable<Manufacturer> GetCategoryManufacturers(int shopID, int categoryID)
        {
            return _shopProductRepository.Find(q => q.ShopID == shopID && q.Product.CategoryID == categoryID).Select(q => q.Product.Manufacturer);
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

    }
}