using Domain.Entity.Articles;
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
    public class ArticleConfig: BaseEntity
    {
        [Display(Name = "Текстовый инфоблок")]
        [StringLength(255, ErrorMessage = "Максимальная длина - 255")]
        public string Slogan { get; set; }

        [Display(Name = "Наилучшная статья (указать ID)")]
        public int? Best1Id { get; set; }

        [Display(Name = "Лучшая статья 1 (указать ID)")]
        public int? Best2Id { get; set; }

        [Display(Name = "Лучшая статья 2 (указать ID)")]
        public int? Best3Id { get; set; }

        [Display(Name = "Лучшая статья 3 (указать ID)")]
        public int? Best4Id { get; set; }


        [ForeignKey("Best1Id")]
        public virtual Article Best1 { get; set; }

        [ForeignKey("Best2Id")]
        public virtual Article Best2 { get; set; }

        [ForeignKey("Best3Id")]
        public virtual Article Best3 { get; set; }

        [ForeignKey("Best4Id")]
        public virtual Article Best4 { get; set; }
    }
}
