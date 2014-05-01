using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class CatalogSecondCategoryConfiguration: EntityTypeConfiguration<CatalogSecondCategory>
    {
        public CatalogSecondCategoryConfiguration()
        {
            ToTable(TableMap.CatalogSecondCategoryTable);
            HasKey(c => c.Id);

            Property(c => c.Name).IsUnicode().IsRequired().HasMaxLength(64);
            Property(c => c.CategoryId).IsRequired();
            Property(c => c.isActive).IsRequired();
        }
    }
}