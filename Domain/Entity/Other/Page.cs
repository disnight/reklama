using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entity.Shared;

namespace Domain.Entity.Other
{
    public class Page: BaseEntity
    {
        [Display(Name = "Название")]
        [StringLength(128, ErrorMessage = "Максимальная длина - 128 символов")]
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [StringLength(250000, ErrorMessage = "Максимальная длина - 250000 символов")]
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Включен?")]
        public bool IsActive { get; set; }

        /* TODO: todo */
        [HiddenInput(DisplayValue = false)]
        public int PageTemplateId { get; set; }

        [Display(Name = "Строка названия для ЧПУ")]
        [StringLength(200, ErrorMessage = "Максимальная длина поля - 200 символов")]
        public string Slug { get; set; }
    }
}
