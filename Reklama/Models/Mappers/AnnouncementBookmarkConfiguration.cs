using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;

namespace Reklama.Models.Mappers
{
    public class AnnouncementBookmarkConfiguration: EntityTypeConfiguration<AnnouncementBookmark>
    {
        public AnnouncementBookmarkConfiguration()
        {
            HasKey(t => t.Id);

            Property(t => t.AnnouncementId).IsRequired();
            Property(t => t.UserId).IsRequired();

            HasRequired(t => t.Announcement);
            HasRequired(t => t.UserProfile);
        }
    }
}