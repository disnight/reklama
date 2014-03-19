using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Repository.Catalogs;
using Domain.Entity.Catalogs;
using Reklama.Models.Shared;

namespace Reklama.Models.Catalog
{
    public class ShopModel: BaseModel<Shop>, IShopRepository
    {
        public bool IsExistShopByCurrentUser(int userId)
        {
            var shop = ReadActivesQuery().Where(s => s.UserId == userId).FirstOrDefault();
            return shop == null ? false : true;
        }

        public IQueryable<Shop> ReadActivesQuery()
        {
            return Context.Set<Shop>().Where(s => s.IsActive == true);
        }


        public int ReadShopIdByUserId(int id)
        {
            return ReadActivesQuery().First(s => s.UserId == id).Id;
        }
    }
}