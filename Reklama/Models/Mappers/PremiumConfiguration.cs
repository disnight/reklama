using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class PremiumConfiguration: EntityTypeConfiguration<Premium>
    {
        public PremiumConfiguration()
        {
            ToTable(TableMap.PremiumTable);
            HasKey(p => p.Id);
            Property(p => p.Cost).IsRequired();
            Property(p => p.Description).HasMaxLength(255).IsRequired().IsUnicode();
            Property(p => p.Lifetime).IsRequired();
            Property(p => p.Name).HasMaxLength(32).IsRequired().IsUnicode();
        }
    }
}