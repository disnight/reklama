using Domain.Entity.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public class ArticleConfigConfiguration: EntityTypeConfiguration<ArticleConfig>
    {
        public ArticleConfigConfiguration()
        {
            ToTable(TableMap.ArticleConfigTable);
            HasKey(a => a.Id);

            Property(a => a.Slogan).HasMaxLength(255).IsUnicode();
        }
    }
}