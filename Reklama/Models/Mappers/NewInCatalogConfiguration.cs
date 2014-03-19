using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class NewInCatalogConfiguration: EntityTypeConfiguration<NewInCatalog>
    {
        public NewInCatalogConfiguration()
        {
            ToTable(TableMap.NewInCatalogTable);
            HasKey(n => n.Id);
            Property(n => n.SubsectionId).IsRequired();
        }
    }
}