using System.Collections.Generic;

namespace Reklama.ViewModels.Catalog
{
    public class ParametersSectionViewModel
    {
        public Data.Entities.CategoryParametersSection Section { get; set; }
        public IEnumerable<ParameterViewModel> Parameters { get; set; }
    }
}