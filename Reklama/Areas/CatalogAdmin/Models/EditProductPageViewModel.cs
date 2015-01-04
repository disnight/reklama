using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Reklama.Data.Entities;
using Reklama.Data.Models;

namespace Reklama.Areas.CatalogAdmin.Models
{
    public class EditProductPageViewModel
    {
        public Product Product { get; set; }
        public SelectList Groups { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Manufacturers { get; set; }
        public List<IGrouping<CategoryParametersSection, CommonParameterObject>> Sections { get; set; }

        public String[] Photos { get; set; } 
    }
}