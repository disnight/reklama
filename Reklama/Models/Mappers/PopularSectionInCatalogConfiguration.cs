using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class PopularSectionInCatalogConfiguration: EntityTypeConfiguration<PopularSectionInCatalog>
    {
        public PopularSectionInCatalogConfiguration()
        {
            ToTable(TableMap.PopularSectionInCatalogTable);
            HasKey(p => p.Id);
            Property(p => p.SectionId).IsRequired();
        }
    }
}