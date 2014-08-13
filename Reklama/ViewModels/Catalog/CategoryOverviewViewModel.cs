using System.Collections.Generic;
using Reklama.Models.ViewModels.Catalog;

namespace Reklama.ViewModels.Catalog
{
    public class CategoryOverviewViewModel
    {
        public PagedList.IPagedList<Data.Entities.Product> Products { get; set; }
        public Data.Entities.Category CurrentCategory { get; set; }
        public ProductsFilterParams Filter { get; set; }
        public IEnumerable<Data.Entities.Manufacturer> Manufacturers { get; set; }
    }
}