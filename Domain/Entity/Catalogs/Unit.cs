using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Catalogs
{
    public class Unit: BaseEntity
    {
        [StringLength(16, ErrorMessage = "Максимальная длина - 16 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
