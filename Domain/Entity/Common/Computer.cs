using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entity.Shared;

namespace Domain.Entity.Common
{
    public class Computer : BaseEntity
    {
        [Required]
        [Display(Name = "Ключ компьютера")]
        [HiddenInput]
        public string Key { get; set; }

        public virtual ICollection<ComputerAnnouncementRef> ComputerAnnouncementRefs { get; set; }
        public virtual ICollection<ComputerRealtyRef> ComputerRealtyRefs { get; set; }
    }
}
