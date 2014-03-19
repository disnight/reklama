using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.SortModels
{
    public class PagerSortModel
    {
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PagerSortModel()
        {
            CurrentPage = 1;
            PageSize = ProjectConfiguration.Get.ItemsOnPage[0];
        }
    }
}