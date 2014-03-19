using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Articles;

namespace Reklama.Models.Mappers
{
    public class ArticleCommentConfiguration: EntityTypeConfiguration<ArticleComment>
    {
        public ArticleCommentConfiguration()
        {
            HasKey(a => a.Id);
            ToTable(TableMap.ArticleCommentTable);
            Property(a => a.ArticleId).IsRequired();
            Property(a => a.UserId).IsRequired();
            Property(a => a.Comment).HasMaxLength(1000).IsUnicode().IsRequired();
            Property(a => a.CreatedAt).IsRequired();
        }
    }
}