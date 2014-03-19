using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Admin
{
    public class AnnouncementConfig: BaseEntity
    {
        [Display(Name="Текстовый инфоблок")]
        [StringLength(255, ErrorMessage = "Максимальная длина - 255")]
        public string Slogan { get; set; }

        [Display(Name="Премиум 1")]
        public int Premium1Id { get; set; }

        [Display(Name = "Премиум 2")]
        public int Premium2Id { get; set; }

        [Display(Name = "Премиум 3")]
        public int Premium3Id { get; set; }



        [ForeignKey("Premium1Id")]
        public virtual Premium Premium1 { get; set; }

        [ForeignKey("Premium2Id")]
        public virtual Premium Premium2 { get; set; }

        [ForeignKey("Premium3Id")]
        public virtual Premium Premium3 { get; set; }
    }
}
