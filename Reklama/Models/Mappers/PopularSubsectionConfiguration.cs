using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class PopularSubsectionConfiguration: EntityTypeConfiguration<PopularSubsection>
    {
        public PopularSubsectionConfiguration()
        {
            ToTable(TableMap.PopularSubsectionTable);
            HasKey(p => p.Id);
            Property(p => p.ArticleId).IsRequired();
        }
    }
}