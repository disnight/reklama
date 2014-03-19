using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class UnitConfiguration : EntityTypeConfiguration<Unit>
    {
        public UnitConfiguration()
        {
            ToTable(TableMap.UnitTable);
            HasKey(u => u.Id);

            Property(u => u.Name).IsRequired().IsUnicode().HasMaxLength(16);
        }
    }
}