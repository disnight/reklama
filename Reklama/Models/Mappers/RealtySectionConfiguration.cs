using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Domain.Entity.Realty;

namespace Reklama.Models.Mappers
{
    public class RealtySectionConfiguration: EntityTypeConfiguration<RealtySection>
    {
        public RealtySectionConfiguration()
        {
            ToTable(TableMap.RealtySectionTable);
            HasKey(r => r.Id);
            Property(r => r.Name).IsRequired().HasMaxLength(32).IsUnicode();
        }
    }
}