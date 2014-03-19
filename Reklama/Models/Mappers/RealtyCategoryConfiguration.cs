using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class RealtyCategoryConfiguration: EntityTypeConfiguration<RealtyCategory>
    {
        public RealtyCategoryConfiguration()
        {
            ToTable(TableMap.RealtyCategoryTable);
            HasKey(r => r.Id);
            Property(r => r.Name).IsRequired().HasMaxLength(32).IsUnicode();
        }
    }
}