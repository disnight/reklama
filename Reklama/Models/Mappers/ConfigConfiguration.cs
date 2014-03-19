using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class ConfigConfiguration: EntityTypeConfiguration<Config>
    {
        public ConfigConfiguration()
        {
            ToTable(TableMap.ConfigTable);
            HasKey(c => c.Id);
            Property(c => c.Description).IsRequired().HasMaxLength(255).IsUnicode();
            Property(c => c.IsEnable).IsOptional();
            Property(c => c.Name).IsRequired().HasMaxLength(32);
            Property(c => c.Value).IsOptional().HasMaxLength(255);
            Property(c => c.IsEnableValue).IsRequired();
        }
    }
}