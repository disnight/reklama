using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository.Shared;
using Domain.Entity.Catalogs;

namespace Domain.Repository.Catalogs
{
    public interface IShopCommentRepository : IRepository<ShopComment>
    {
        IQueryable<ShopComment> ReadByUser(int userId);
        IQueryable<ShopComment> ReadByRealty(int shopId);
    }
}
