using Reklama.Data.Entities;

namespace Reklama.Data.Models
{
    public class CommonParameterObject
    {
        public CategoryParametersSection Section { get; set; }
        public  CategoryParameter Parameter { get; set; }
        public ProductParameterValue ParameterValue { get; set; }
    }
}