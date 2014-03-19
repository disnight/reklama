using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Other
{
    public class MenuPageRef: BaseEntity
    {
        [Display(Name = "Меню")]
        public int MenuId { get; set; }

        [Display(Name = "Страница")]
        public int PageId { get; set; }

        [Range(0, 255, ErrorMessage = "Допустимые значения в диапазоне 0 <= x <= 255")]
        public int Priority { get; set; }


        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        [ForeignKey("PageId")]
        public virtual Page Page { get; set; }
    }
}
