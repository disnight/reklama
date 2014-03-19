using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using Domain.Entity.Articles;


namespace Domain.Entity.Announcements
{
    public class Subsection: BaseEntity
    {
        [Required(ErrorMessage = "Выберите подраздел")]
        [StringLength(64, ErrorMessage = "Минимальная длина - 3, максимальная - 64 символа", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        public bool IsNew { get; set; }


        public virtual Section Section { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}