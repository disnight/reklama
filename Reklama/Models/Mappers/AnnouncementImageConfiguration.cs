using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;

namespace Reklama.Models.Mappers
{
    public class AnnouncementImageConfiguration: EntityTypeConfiguration<AnnouncementImage>
    {
        public AnnouncementImageConfiguration()
        {
            HasKey(a => a.Id);

            Property(a => a.Link).IsRequired().HasMaxLength(255).IsUnicode();
            Property(a => a.IsTitular).IsOptional();
        }
     }
}