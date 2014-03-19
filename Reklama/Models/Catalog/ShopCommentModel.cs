using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Reklama.Models.Shared;
using Domain.Repository.Catalogs;

namespace Reklama.Models.Catalog
{
    public class ShopCommentModel: BaseModel<ShopComment>, IShopCommentRepository 
    {
        public IQueryable<ShopComment> ReadByUser(int userId)
        {
            return Context.Set<ShopComment>().Where(sc => sc.UserId == userId);
        }

        public IQueryable<ShopComment> ReadByRealty(int shopId)
        {
            return Context.Set<ShopComment>().Where(sc => sc.ShopId == shopId);
        }
    }
}