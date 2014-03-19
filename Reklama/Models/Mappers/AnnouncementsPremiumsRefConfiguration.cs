using Domain.Entity.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public class AnnouncementsPremiumsRefConfiguration: EntityTypeConfiguration<AnnouncementsPremiumsRef>
    {
        public AnnouncementsPremiumsRefConfiguration()
        {
            ToTable(TableMap.AnnouncementsPremiumsRefTable);
            HasKey(r => r.Id);

            Property(r => r.PremiumId).IsRequired();
            Property(r => r.AdId).IsRequired();
            Property(r => r.AdSectionId).IsRequired();
            Property(r => r.CreatedAt).IsRequired();
            Property(r => r.ExpiresAt).IsRequired();
        }
    }
}