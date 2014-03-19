using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;

namespace Reklama.Models.Mappers
{
    public class AnnouncementCommentConfiguration: EntityTypeConfiguration<AnnouncementComment>
    {
        public AnnouncementCommentConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.AnnouncementId).IsRequired();
            Property(c => c.Comment).IsRequired().HasMaxLength(1000).IsUnicode();
            Property(c => c.CreatedAt).IsRequired();
            Property(c => c.IsActive).IsRequired();
            Property(c => c.UserId).IsRequired();
        }
    }
}