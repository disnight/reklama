using System.Collections.Generic;
using System.Linq;
using WebMatrix.WebData;

namespace Reklama.ViewModels.Catalog
{
    public class DetailsViewModel
    {
        public Data.Entities.Product CurrentProduct { get; set; }
        public IEnumerable<Data.Entities.Manufacturer> Manufacturers { get; set; }

        public bool IsInBookmarks
        {
            get { return CurrentProduct.ProductBookmark.Any(q => q.UserID == WebSecurity.CurrentUserId); }
        }

        public IEnumerable<ParametersSectionViewModel> Sections { get; set; }
    }
}