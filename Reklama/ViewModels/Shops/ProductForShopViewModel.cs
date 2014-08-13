using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using Reklama.Data.Entities;
using Reklama.Models.ViewModels.Catalog;

namespace Reklama.ViewModels.Shops
{
    public class ProductForShopViewModel
    {
        public FilterParams Filter { get; set; }
        public Shop Shop { get; set; }
        public decimal MonthlyFee { get; set; }
        public IEnumerable<ShopProduct> ShopProducts { get; set; } 
        public IPagedList<Product> Products { get; set; }
        public IPagedList<ShopProduct> ShopProductsFiltered { get; set; } 

        public SelectList Groups { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Manufacturers { get; set; } 
    }
}