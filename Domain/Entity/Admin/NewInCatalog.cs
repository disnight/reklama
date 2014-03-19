using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Entity.Shared;

namespace Domain.Entity.Admin
{
    public class NewInCatalog: BaseEntity
    {
        public int SubsectionId { get; set; }

        [ForeignKey("SubsectionId")]
        public virtual Subsection Subsection { get; set; }
    }
}