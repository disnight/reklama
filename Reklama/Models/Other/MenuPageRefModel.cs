using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Other;
using Domain.Repository.Other;
using Reklama.Models.Shared;

namespace Reklama.Models.Other
{
    public class MenuPageRefModel: BaseModel<MenuPageRef>, IMenuPageRefRepository
    {
        public override IQueryable<MenuPageRef> Read()
        {
            return Context.Set<MenuPageRef>().Include("Menu").Include("Page");
        }

        public override MenuPageRef Read(int id)
        {
            return Context.Set<MenuPageRef>().Include("Menu").Include("Page").First(r => r.Id == id);
        }
    }
}