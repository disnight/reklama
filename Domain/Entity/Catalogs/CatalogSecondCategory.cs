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
    public class CatalogSecondCategory: BaseEntity
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(64, ErrorMessage = "Максимальная длина поля - 64 символа")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Выберите категорию")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Введите стоимость для магазина")]
        [DataType(DataType.Currency)]
        [Display(Name = "Стоимость для магазина")]
        public decimal Price { get; set; }

        [Display(Name = "Новый раздел?")]
        public bool isNew { get; set; }

        [Display(Name = "Is Active?")]
        public bool isActive { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CatalogCategory Category { get; set; }


        public virtual ICollection<CatalogParamSubsection> ParamSubsections { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<CatalogThirdCategory> CatalogThirdCategories { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
