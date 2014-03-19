using Domain.Entity.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public class AnnouncementConfigConfiguration: EntityTypeConfiguration<AnnouncementConfig>
    {
        public AnnouncementConfigConfiguration()
        {
            ToTable(TableMap.AnnouncementConfigTable);
            HasKey(ac => ac.Id);

            Property(ac => ac.Slogan).HasMaxLength(255).IsUnicode();
            Property(ac => ac.Premium1Id).IsRequired();
            Property(ac => ac.Premium2Id).IsRequired();
            Property(ac => ac.Premium3Id).IsRequired();
        }
    }
}