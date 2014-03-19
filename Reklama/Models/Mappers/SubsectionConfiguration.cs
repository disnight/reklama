using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;

namespace Reklama.Models.Mappers
{
    public class SubsectionConfiguration: EntityTypeConfiguration<Subsection>
    {
        public SubsectionConfiguration()
        {
            ToTable(TableMap.SubsectionTable);
            HasKey(s => s.Id);
            Property(s => s.Name).HasMaxLength(64).IsRequired().IsUnicode(true);
            Property(s => s.IsNew).IsRequired();

            HasMany(s => s.Announcements);
        }
    }
}