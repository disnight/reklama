using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;

namespace Domain.Entity.Announcements
{
    public class Section : BaseEntity
    {
        [Required]
        [StringLength(64, ErrorMessage = "Минимальная длина - 3, максимальная - 64 символа", MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<Subsection> Subsections { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}