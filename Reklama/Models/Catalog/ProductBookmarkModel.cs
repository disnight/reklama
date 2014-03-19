using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Catalog
{
    public class ProductBookmarkModel : BaseModel<ProductBookmark>, IProductBookmarkRepository
    {
        public IQueryable<Product> ReadProductsByUser(int userId)
        {
            return from a in Context.Set<ProductBookmark>()
                .Include("Product")
                .Include("UserProfile")
                   where a.UserId.Equals(userId)
                   orderby a.Id descending
                   select a.Product;
        }

        public bool IsIsset(int userId, int productId)
        {
            return Read().Any(a => a.ProductId.Equals(productId) && a.UserId.Equals(userId));
        }

        public ProductBookmark Read(int userId, int productId)
        {
            return Context.Set<ProductBookmark>()
                .Include("Product")
                .Include("UserProfile")
                .Single(a => a.ProductId.Equals(productId) && a.UserId.Equals(userId));
        }
    }
}