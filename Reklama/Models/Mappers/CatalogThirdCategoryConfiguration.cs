using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class CatalogThirdCategoryConfiguration: EntityTypeConfiguration<CatalogThirdCategory>
    {
        public CatalogThirdCategoryConfiguration()
        {
            ToTable(TableMap.CatalogThirdCategoryTable);
            HasKey(c => c.Id);

            Property(c => c.Name).HasMaxLength(64).IsRequired().IsUnicode();
            Property(c => c.SecondCategoryId).IsRequired();
        }
    }
}