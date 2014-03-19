using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.ViewModels.Catalog
{
    public class ProductPriceCurrencyViewModel
    {
        public decimal PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
    }
}