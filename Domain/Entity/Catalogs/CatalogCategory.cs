using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Catalogs
{
    public class CatalogCategory: BaseEntity
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(64, ErrorMessage = "Максимальная длина поля - 64 символа")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        //public virtual ICollection<CatalogParamSubsection> ParamSubsections { get; set; }

        public virtual ICollection<CatalogSecondCategory> SecondCategories { get; set; }
        public virtual ICollection<ProductSectionParamRef> ProductCategoryParams { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
