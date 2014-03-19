using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Other;

namespace Reklama.Models.Mappers
{
    public class MenuConfiguration: EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        {
            ToTable(TableMap.MenuTable);
            HasKey(m => m.Id);

            Property(m => m.Name).HasMaxLength(32).IsRequired().IsUnicode();
            Property(m => m.Description).HasMaxLength(255).IsOptional().IsUnicode();
            Property(m => m.Image).HasMaxLength(255).IsOptional().IsUnicode();
        }
    }
}