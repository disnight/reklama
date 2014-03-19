using Domain.Entity.Announcements;
using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Admin
{
    public class PopularAnnouncement: BaseEntity
    {
        public int AnnouncementId { get; set; }
        [ForeignKey("AnnouncementId")]
        public virtual Announcement Announcement { get; set; }
    }
}
