using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Realty
{
    public class RealtyPayment : BaseEntity
    {
        public double Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RealtyId { get; set; }
        public Guid TransactionId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("RealtyId")]
        public virtual Realty Realty { get; set; }
    }
}
