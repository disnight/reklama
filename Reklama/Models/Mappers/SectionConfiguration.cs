using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;

namespace Reklama.Models.Mappers
{
    public class SectionConfiguration: EntityTypeConfiguration<Section>
    {
        public SectionConfiguration()
        {
            ToTable(TableMap.SectionTable);
            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(64).IsUnicode();

            HasMany(s => s.Subsections);
            HasMany(s => s.Announcements);
        }
    }
}