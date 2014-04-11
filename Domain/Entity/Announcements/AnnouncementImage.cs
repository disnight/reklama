using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Announcements
{
    public class AnnouncementImage: BaseEntity
    {
        public string Link { get; set; }
        public int AnnouncementId { get; set; }
        
        public bool? IsTitular { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("AnnouncementId")]
        public virtual Announcement Announcement { get; set; }
    }
}
