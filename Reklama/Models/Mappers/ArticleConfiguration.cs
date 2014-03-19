using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Articles;

namespace Reklama.Models.Mappers
{
    public class ArticleConfiguration: EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            this.HasKey(a => a.Id);
            Property(a => a.CreatedAt).IsRequired();
            Property(a => a.SmallDescription).IsRequired().IsUnicode().HasMaxLength(600);
            Property(a => a.Description).HasMaxLength(100000).IsRequired().IsUnicode();
            Property(a => a.IsActive).IsRequired();
            Property(a => a.Logo).HasMaxLength(255).IsOptional().IsUnicode();
            Property(a => a.Name).HasMaxLength(180).IsRequired().IsUnicode();
            Property(a => a.Slug).HasMaxLength(255).IsUnicode();
            Property(a => a.ViewsCount).IsRequired();
        }
    }
}