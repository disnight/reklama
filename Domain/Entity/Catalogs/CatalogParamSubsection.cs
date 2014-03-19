using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Catalogs
{
    public class CatalogParamSubsection: BaseEntity
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(128, ErrorMessage = "Максимальная длина - 32 символа")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public int SecondCategoryId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(255, ErrorMessage = "Максимальная длина - 255 символов")]
        [Display(Name = "Название для админа")]
        public string Slogan { get; set; }

        [ForeignKey("SecondCategoryId")]
        public virtual CatalogSecondCategory SecondCategory { get; set; }

        public virtual ICollection<ProductSubsectionParamRef> ProductSubsectionParamRefs { get; set; }
    }
}
