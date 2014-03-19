using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Admin
{
    public class ActualArticle: BaseEntity
    {
        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Domain.Entity.Articles.Article Article { get; set; }
    }
}