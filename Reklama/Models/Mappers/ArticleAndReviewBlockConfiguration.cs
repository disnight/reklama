using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class ArticleAndReviewBlockConfiguration: EntityTypeConfiguration<ArticleAndReviewBlock>
    {
        public ArticleAndReviewBlockConfiguration()
        {
            ToTable(TableMap.ArticleAndReviewBlockTable);
            HasKey(a => a.Id);
            Property(a => a.ArticleId).IsRequired();
        }
    }
}