using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Common;

namespace Reklama.Models.Mappers
{
    public class ComputerConfiguration : EntityTypeConfiguration<Computer>
    {
        public ComputerConfiguration()
        {
            ToTable(TableMap.ComputerTable);
            HasKey(p => p.Id);

            Property(p => p.Key).HasMaxLength(50).IsRequired();
        }
    }
}