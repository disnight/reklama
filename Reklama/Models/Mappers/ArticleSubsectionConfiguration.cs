using Domain.Entity.Articles;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public class ArticleSubsectionConfiguration: EntityTypeConfiguration<ArticleSubsection>
    {
        public ArticleSubsectionConfiguration()
        {
            ToTable(TableMap.SubsectionTable);
            HasKey(s => s.Id);
            Property(s => s.Name).HasMaxLength(64).IsRequired().IsUnicode(true);
            Property(s => s.IsNew).IsRequired();

            HasMany(s => s.Articles);
        }
    }
}