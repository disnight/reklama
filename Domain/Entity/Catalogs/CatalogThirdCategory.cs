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
    public class CatalogThirdCategory: BaseEntity
    {
        [Required]
        [StringLength(64, ErrorMessage = "Максимальная длина поля - 64 символа")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Выберите категорию")]
        public int SecondCategoryId { get; set; }


        [ForeignKey("SecondCategoryId")]
        public virtual CatalogSecondCategory SecondCategory { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
