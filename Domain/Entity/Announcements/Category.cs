using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;

namespace Domain.Entity.Announcements
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(64, ErrorMessage = "Минимальная длина - 3, максимальная - 64 символа", MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Announcement> Announcements { get; set; } 
    }
}