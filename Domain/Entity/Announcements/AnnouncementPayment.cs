using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Announcements
{
    public class AnnouncementPayment: BaseEntity
    {
        public int AnnouncementId { get; set; }
        public float Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TransactionId { get; set; }
        public int UserId { get; set; }
    }
}
