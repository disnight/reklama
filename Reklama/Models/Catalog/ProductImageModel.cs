using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;

namespace Reklama.Models.Catalog
{
    public class ProductImageModel: BaseModel<ProductImage>, IProductImageRepository
    {
        public void Delete(int id, string image)
        {
            var imageWithoutPath = image.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
            var productImage =
                Context.Set<ProductImage>().Where(p => p.ProductId == id).First(
                    a => a.Link.Equals(imageWithoutPath));
            Delete(productImage);
        }
    }
}