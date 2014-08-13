using System.Collections.Generic;
using PagedList;

namespace Reklama.ViewModels.Shops
{
    public class ShopProductsViewModel
    {
        public ShopProductsFilter Filter { get; set; }
        public Data.Entities.Category Category { get; set; }
        public IPagedList<Data.Entities.Product> Products { get; set; } 
    }
}