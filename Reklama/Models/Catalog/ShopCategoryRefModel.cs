using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;

namespace Reklama.Models.Catalog
{
    public class ShopCategoryRefModel: BaseModel<ShopCategoryRef>, IShopCategoryRefRepository
    {

        public IQueryable<ShopCategoryRef> ReadByShop(int shopId)
        {
            return ((ReklamaContext)Context).ShopCategoryRefs.Where(x => x.ShopId == shopId);
        }

        public void Delete(int categoryId, int shopId)
        {
            var s = ((ReklamaContext)Context).ShopCategoryRefs.Where(x => x.ShopId == shopId && x.CategoryId == categoryId).FirstOrDefault();
            base.Delete(s);
        }
    }
}