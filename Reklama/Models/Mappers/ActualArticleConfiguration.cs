using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class ActualArticleConfiguration: EntityTypeConfiguration<ActualArticle>
    {
        public ActualArticleConfiguration()
        {
            ToTable(TableMap.ActualArticleTable);
            HasKey(a => a.Id);
            Property(a => a.ArticleId).IsRequired();
        }
    }
}