using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Catalogs
{
    public class ProductParam: BaseEntity
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(128, ErrorMessage = "Максимальная длина - 128 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}
