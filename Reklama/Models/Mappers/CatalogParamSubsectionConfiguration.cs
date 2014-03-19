using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class CatalogParamSubsectionConfiguration: EntityTypeConfiguration<CatalogParamSubsection>
    {
        public CatalogParamSubsectionConfiguration()
        {
            ToTable(TableMap.CatalogParamSubsectionTable);
            HasKey(c => c.Id);

            Property(c => c.Name).IsUnicode().IsRequired().HasMaxLength(128);
            Property(c => c.Slogan).HasMaxLength(255).IsUnicode().IsRequired();
            Property(c => c.SecondCategoryId).IsRequired();
        }
    }
}