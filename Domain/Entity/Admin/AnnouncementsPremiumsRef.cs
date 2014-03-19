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
    public class AnnouncementsPremiumsRef: BaseEntity
    {
        public int AdId { get; set; }
        public int AdSectionId { get; set; }
        public int PremiumId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        [ForeignKey("AdId")]
        public virtual Announcement Announcement { get; set; }

        [ForeignKey("AdSectionId")]
        public virtual Section AnnouncementSection { get; set; }

        [ForeignKey("PremiumId")]
        public virtual Premium Premium { get; set; }
    }
}
