using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.ViewModels.Catalog
{
    public class ProductsFilterParams
    {
        public int SecondCategoryId { get; set; }
        public int? ThirdCategoryId { get; set; }
        public SortOrderParams SortOrder { get; set; }
        public SortOptionsParams SortOptions { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public ProductsFilterParams()
        {
            Page = 1;
            PageSize = ProjectConfiguration.Get.ItemsOnPage[0];
            SortOrder = SortOrderParams.Ascending;
            SortOptions = SortOptionsParams.ByName;
        }
    }
}