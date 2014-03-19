using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;
using Reklama.Models.Shared;

namespace Reklama.Models.Announcements
{
    public class CategoryModel : BaseModel<Category>, ICategoryRepository
    {
        public Category ReadByName(string name)
        {
            return Context.Set<Category>().First(c => c.Name.Equals(name));
        }
    }
}