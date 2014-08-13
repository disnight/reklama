using System.Collections.Generic;
using System.Linq;
using Reklama.Data.Entities;

namespace Reklama.ViewModels.Shops
{
    public class CategoriesViewModel
    {
        public IEnumerable<IGrouping<Group, Category>> Groups { get; set; }
        public int ShopID { get; set; }
    }
}