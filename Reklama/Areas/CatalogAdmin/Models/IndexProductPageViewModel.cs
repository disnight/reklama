using System.Collections.Generic;
using System.Web.Mvc;
using Reklama.Data.Entities;

namespace Reklama.Areas.CatalogAdmin.Models
{
    public class IndexProductPageViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Group> Group { get; set; }

        public int CurrentID = 9;
    }
}