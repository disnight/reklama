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
    public class ProductSubsectionParamRef: BaseEntity
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Выберите параметр")]
        public int ParamId { get; set; }

        [Display(Name = "Выберите подраздел параметра")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int SubsectionId { get; set; }


        [ForeignKey("ParamId")]
        public virtual ProductParam Param { get; set; }

        [ForeignKey("SubsectionId")]
        public virtual CatalogParamSubsection Subsection { get; set; }
    }
}
