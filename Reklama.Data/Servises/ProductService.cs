using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Reklama.Data.Base;
using Reklama.Data.Entities;
using Reklama.ViewModels.Catalog;

namespace Reklama.Data.Servises
{

    public class ProductService
    {
        private readonly ReklamaCustomEntities _context;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductFeedback> _productFeedbacksRepository;
        public ProductService()
        {
            _context = new ReklamaCustomEntities();
            _groupRepository = new Repository<Group>(_context);
            _productRepository = new Repository<Product>(_context);
            _categoryRepository = new Repository<Category>(_context);
            _productFeedbacksRepository = new Repository<ProductFeedback>(_context);
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return _groupRepository.GetAll();
        }

        public IEnumerable<Product> GetProducts(int categoryID)
        {
            return _productRepository.Find(q => categoryID == 0 || q.CategoryID == categoryID);
        }

        public Category GetCategory(int categoryID)
        {
            return _categoryRepository.Find(q => q.ID == categoryID).FirstOrDefault();
        }

        public Product GetProduct(long ID)
        {
            return _productRepository.Find(q => q.ID == ID).FirstOrDefault();
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
    }
}