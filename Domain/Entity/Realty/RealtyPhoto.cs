using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Realty
{
    public class RealtyPhoto : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string Link { get; set; }
        public int RealtyId { get; set; }
        public bool? IsTitular { get; set; }

        [ForeignKey("RealtyId")]
        public virtual Realty Realty { get; set; }
    }
}
