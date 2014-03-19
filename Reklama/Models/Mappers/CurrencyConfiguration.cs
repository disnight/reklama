using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class CurrencyConfiguration: EntityTypeConfiguration<Currency>
    {
        public CurrencyConfiguration()
        {
            ToTable(TableMap.CurrencyTable);
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(32).IsUnicode();
            Property(c => c.Rate).IsRequired();
        }
    }
}