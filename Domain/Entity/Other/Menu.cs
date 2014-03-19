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
    public class Menu: BaseEntity
    {
        [StringLength(32, ErrorMessage = "Максимальная длина поля - 32 символа")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Максимальная длина поля - 255 символов")]
        public string Description { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        public virtual ICollection<MenuPageRef> MenuPageRefs { get; set; }
    }
}