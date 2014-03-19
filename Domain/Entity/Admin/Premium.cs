using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Admin
{
    public class Premium: BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(32, ErrorMessage = "Максимальная длина - 32")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(255, ErrorMessage = "Максимальная длина - 255")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Currency)]
        [Display(Name = "Цена")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Duration)]
        [Display(Name = "Время действия премиума в часах")]
        public int Lifetime { get; set; }
    }
}
