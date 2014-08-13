using System.Collections.Generic;
using PagedList;
using Reklama.Data.Entities;

namespace Reklama.Models.ViewModels.Catalog
{
    public class ProductShopsPageViewModel
    {
        public IPagedList<ShopProduct> Shops { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public ProductsFilterParams Filter { get; set; }
    }
}