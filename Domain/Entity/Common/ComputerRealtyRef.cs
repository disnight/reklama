using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Announcements;
using Domain.Entity.Other;
using Domain.Entity.Shared;

namespace Domain.Entity.Common
{
    public class ComputerRealtyRef : BaseEntity
    {
        [Display(Name = "Объявление")]
        public int RealtyId { get; set; }

        [Display(Name = "Компьютер")]
        public int ComputerId { get; set; }


        [ForeignKey("ComputerId")]
        public virtual Computer Computer { get; set; }

        [ForeignKey("RealtyId")]
        public virtual Realty.Realty Realty { get; set; }
    }
}
