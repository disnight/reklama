using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entity.Shared
{
    public class SearchProviderEntity: BaseEntity
    {
        [AllowHtml]
        [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения")]
        [StringLength(180, ErrorMessage = "Минимальная длина - 6, максимальная - 180 символов", MinimumLength = 6)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Поле 'Краткое описание' обязательно для заполнения")]
        [StringLength(600, ErrorMessage = "Минимальная длина - 6, максимальная - 600 символа", MinimumLength = 6)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Краткое описание")]
        public string SmallDescription { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Поле 'Описание' обязательно для заполнения")]
        [StringLength(100000, ErrorMessage = "Минимальная длина - 20, максимальная - 4000 символов", MinimumLength = 20)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Подробное описание")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }
    }
}
