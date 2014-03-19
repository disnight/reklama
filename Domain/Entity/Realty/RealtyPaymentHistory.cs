using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Realty
{
    public class RealtyPaymentHistory : BaseEntity
    {
        public DateTime ComplectedAt { get; set; }
        public double Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public string PaysystemTransactionId { get; set; }
        public int RealtyId { get; set; }
        public Guid TransactionId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("RealtyId")]
        public virtual Realty Realty { get; set; }
    }
}
