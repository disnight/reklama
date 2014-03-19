using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class CatalogCategoryConfiguration: EntityTypeConfiguration<CatalogCategory>
    {
        public CatalogCategoryConfiguration()
        {
            ToTable(TableMap.CatalogCategoryTable);
            HasKey(c => c.Id);
            Property(c => c.Name).IsUnicode().IsRequired().HasMaxLength(64);
        }
    }
}