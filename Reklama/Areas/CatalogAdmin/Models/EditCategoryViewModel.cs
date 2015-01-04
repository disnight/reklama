using System.Collections.Generic;
using System.Web.Mvc;
using Reklama.Data.Entities;

namespace Reklama.Areas.CatalogAdmin.Models
{
    public class EditCategoryViewModel
    {
        public Category Category { get; set; }

        public IEnumerable<CategoryParametersSection> Sections { get; set; }
        public SelectList Groups { get; set; }
    }
}