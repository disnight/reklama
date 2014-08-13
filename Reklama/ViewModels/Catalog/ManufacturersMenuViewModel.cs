using System.Collections.Generic;

namespace Reklama.ViewModels.Catalog
{
    public class ManufacturersMenuViewModel
    {
        public IEnumerable<Data.Entities.Manufacturer> Manufacturers { get; set; }
        public Data.Entities.Category CurrentCategory { get; set; }
    }
}