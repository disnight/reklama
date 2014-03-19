using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class CityConfiguration: EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            ToTable(TableMap.CityTable);
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(32);
        }
    }
}