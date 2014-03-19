using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.ViewModels.Catalog
{
    public class FilterParams
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SecondCategoryId { get; set; }
        public int ThirdCategoryId { get; set; }
        public int Page { get; set; }

        public FilterParams()
        {
            Page = 1;
        }
    }
}