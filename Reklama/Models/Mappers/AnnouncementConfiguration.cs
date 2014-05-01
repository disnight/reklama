using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;

namespace Reklama.Models.Mappers
{
    public class AnnouncementConfiguration: EntityTypeConfiguration<Announcement>
    {
        public AnnouncementConfiguration()
        {
            ToTable(TableMap.AnnouncementTable);
            HasKey(a => a.Id);
            Property(a => a.IsActive).IsRequired();
            Property(a => a.IsAuction).IsRequired();
            Property(a => a.Name).HasMaxLength(180).IsRequired().IsUnicode();
            Property(a => a.Price).IsOptional();
            Property(a => a.SectionId).IsRequired();
            Property(a => a.SmallDescription).HasMaxLength(600).IsRequired().IsUnicode();
            Property(a => a.SubsectionId).IsRequired();
            Property(a => a.UserId).IsRequired();
            Property(a => a.CreatedAt).IsRequired();
            Property(a => a.Description).HasMaxLength(100000).IsRequired().IsUnicode();
            Property(a => a.CityId).IsOptional();
            Property(a => a.ContactName).IsOptional();
            Property(a => a.ContactEmail).IsOptional();
            Property(a => a.CurrencyId).IsRequired();
            Property(a => a.ViewsCount).IsRequired();
            Property(a => a.IsDisplayPhone).IsRequired();
            Property(a => a.Phone).HasMaxLength(32).IsUnicode();
        }
    }
}