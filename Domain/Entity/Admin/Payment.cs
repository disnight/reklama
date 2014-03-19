using Domain.Entity.Shared;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Admin
{
    public class Payment: BaseEntity
    {
        public decimal Cost { get; set; }
        public int PaysystemInvId { get; set; }
        public int AdId { get; set; }
        public CategorySearch SiteCategory { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentStatus Status { get; set; }
        public int? PremiumId { get; set; }
        public int? SectionId { get; set; }
    }
}
