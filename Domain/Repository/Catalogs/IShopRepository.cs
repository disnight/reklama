using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository.Shared;
using Domain.Entity.Catalogs;

namespace Domain.Repository.Catalogs
{
    public interface IShopRepository: IRepository<Shop>
    {
        bool IsExistShopByCurrentUser(int userId);
        IQueryable<Shop> ReadActivesQuery();
        int ReadShopIdByUserId(int id);
    }
}
