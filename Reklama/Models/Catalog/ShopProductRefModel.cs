using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Catalog
{
    public class ShopProductRefModel: BaseModel<ShopProductRef>, IShopProductRefRepository
    {
        public IQueryable<ShopProductRef> ReadProductsByShop(int shopId)
        {
            return ((ReklamaContext)Context).ShopProductRefs.Where(x => x.ShopId == shopId);
        }

        public IQueryable<ShopProductRef> ReadShopsByProduct(int productId)
        {
            return ((ReklamaContext)Context).ShopProductRefs.Where(x => x.ProductId == productId);
        }

        public IQueryable<ShopProductRef> ReadProductsBySecondCategory(int shopId, int secondCategoryId)
        {
            return ReadProductsByShop(shopId).Where(x => x.Product.SecondCategoryId == secondCategoryId);
        }

        public IQueryable<ShopProductRef> ReadProductsByThirdCategory(int shopId, int thirdCategoryId)
        {
            return ReadProductsByShop(shopId).Where(x => x.Product.ThirdCategoryId == thirdCategoryId);
        }

        public void Delete(int productId, int shopId)
        {
            var s = ((ReklamaContext)Context).ShopProductRefs.Where(x => x.ShopId == shopId && x.ProductId == productId).FirstOrDefault();
            base.Delete(s);
        }
    }
}