using Domain.Entity.Articles;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public class ArticleSectionConfiguration: EntityTypeConfiguration<ArticleSection>
    {
        public ArticleSectionConfiguration()
        {
            ToTable(TableMap.SectionTable);
            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(64).IsUnicode();

            HasMany(s => s.Subsections);
        }
    }
}