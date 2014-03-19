using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.ViewModels.Catalog
{
    public class ProductsShopFilterParams: ProductsFilterParams 
    {
        public int? ShopId { get; set; }
    }
}
