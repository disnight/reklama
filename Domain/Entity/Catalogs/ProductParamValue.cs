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
    public class ProductParamValue: BaseEntity
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Выберите товар")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Выберите параметр")]
        public int ParamId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(1000, ErrorMessage = "Максимальная длина поля - 1000 символов")]
        [Display(Name = "Значение")]
        public string Value { get; set; }

		[Display(Name = "Единица измерения")]
        public int UnitId { get; set; }

        public int? ParamSubsectionId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("ParamId")]
        public virtual ProductParam Param { get; set; }

        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }

        [ForeignKey("ParamSubsectionId")]
        public virtual CatalogParamSubsection ParamSubsection { get; set; }
    }
}
