using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Articles;
using Domain.Entity.Shared;

namespace Domain.Entity.Admin
{
    public class MainPageArticleConfig: BaseEntity
    {
        [Display(Name = "статья 1 (указать ID)")]
        public int? Article1Id { get; set; }

        [Display(Name = "статья 2 (указать ID)")]
        public int? Article2Id { get; set; }

        [Display(Name = "статья 3 (указать ID)")]
        public int? Article3Id { get; set; }

        [Display(Name = "статья 4 (указать ID)")]
        public int? Article4Id { get; set; }


        [ForeignKey("Article1Id")]
        public virtual Article Article1 { get; set; }

        [ForeignKey("Article2Id")]
        public virtual Article Article2 { get; set; }

        [ForeignKey("Article3Id")]
        public virtual Article Article3 { get; set; }

        [ForeignKey("Article4Id")]
        public virtual Article Article4 { get; set; }
    }
}
