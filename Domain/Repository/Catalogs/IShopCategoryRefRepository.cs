using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository.Shared;
using Domain.Entity.Catalogs;

namespace Domain.Repository.Catalogs
{
    public interface IShopCategoryRefRepository: IRepository<ShopCategoryRef>
    {
        IQueryable<ShopCategoryRef> ReadByShop(int shopId);
        void Delete(int categoryId, int shopId);
    }
}
