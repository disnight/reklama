using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Other;
using Domain.Repository.Other;
using Reklama.Models.Shared;

namespace Reklama.Models.Other
{
    public class MenuModel: BaseModel<Menu>, IMenuRepository
    {
        public Menu ReadByName(string name)
        {
            Menu menu;

            try
            {
                menu = Context.Set<Menu>().Include("MenuPageRefs").First(m => m.Name.Equals(name));
            }
            catch
            {
                return null;
            }

            return menu;
        }
    }
}