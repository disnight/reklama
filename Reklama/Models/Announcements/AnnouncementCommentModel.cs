using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;
using Reklama.Models.Shared;

namespace Reklama.Models.Announcements
{
    public class AnnouncementCommentModel: BaseModel<AnnouncementComment>, IAnnouncementCommentRepository
    {
        public IEnumerable<AnnouncementComment> ReadByAnnouncement(int announcementId)
        {
            return Context.Set<AnnouncementComment>()
                .Include("Announcement").Include("UserProfile")
                .Where(c => c.AnnouncementId == announcementId).ToArray();
        }

        public IEnumerable<AnnouncementComment> ReadByUser(int userId)
        {
            return Context.Set<AnnouncementComment>()
                .Include("Announcement").Include("UserProfile")
                .Where(c => c.UserId == userId);
        }
    }
}