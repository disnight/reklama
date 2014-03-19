using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Announcements
{
    public class AnnouncementPaymentHistory: BaseEntity
    {
        public int AnnouncementId { get; set; }
        public DateTime CompletedAt { get; set; }
        public float Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public string PaysystemTransactionId { get; set; }
        public Guid TransactionId { get; set; }
        public int UserId { get; set; }
    }
}
