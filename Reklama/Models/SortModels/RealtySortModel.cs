using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Enums;

namespace Reklama.Models.SortModels
{
    public class RealtySortModel
    {
        public SortOrderParams SortOrder { get; set; }
        public SortOptionsParams SortOptions { get; set; }
        public int? SectionId { get; set; }
        public int? CategoryId { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }

        public RealtySortModel()
        {
            SortOptions = SortOptionsParams.ByDate;
            SortOrder = SortOrderParams.Descending;
            CurrentPage = 1;
            PageSize = ProjectConfiguration.Get.ItemsOnPage[0];
        }
    }
}