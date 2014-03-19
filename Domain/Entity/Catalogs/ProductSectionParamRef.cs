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
    public class ProductSectionParamRef : BaseEntity
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Выберите параметр")]
        public int ParamId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Выберите категорию для параметра")]
        public int CategoryId { get; set; }


        [ForeignKey("ParamId")]
        public virtual ProductParam Param { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CatalogCategory Category { get; set; }
    }
}
