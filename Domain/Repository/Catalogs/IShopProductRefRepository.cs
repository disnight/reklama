using Domain.Entity.Catalogs;
using Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Catalogs
{
    public interface IShopProductRefRepository: IRepository<ShopProductRef>
    {
        IQueryable<ShopProductRef> ReadProductsByShop(int shopId);
        IQueryable<ShopProductRef> ReadProductsBySecondCategory(int shopId, int secondCategory);
        IQueryable<ShopProductRef> ReadProductsByThirdCategory(int shopId, int thirdCategory);
        IQueryable<ShopProductRef> ReadShopsByProduct(int productId);
        void Delete(int productId, int shopId);
    }
}
