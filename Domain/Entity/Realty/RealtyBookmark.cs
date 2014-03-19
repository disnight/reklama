using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Realty
{
    public class RealtyBookmark : BaseEntity
    {
        public int RealtyId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("RealtyId")]
        public virtual Realty Realty { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }
    }
}
