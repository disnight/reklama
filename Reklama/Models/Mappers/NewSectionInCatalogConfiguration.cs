using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class NewSectionInCatalogConfiguration: EntityTypeConfiguration<NewSectionInCatalog>
    {
        public NewSectionInCatalogConfiguration()
        {
            ToTable(TableMap.NewSectionInCatalogTable);
            HasKey(n => n.Id);
            Property(n => n.SectionId).IsRequired();
        }
    }
}