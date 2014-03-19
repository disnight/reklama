using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;
using Reklama.Models.Shared;

namespace Reklama.Models.Announcements
{
    public class AnnouncementBookmarkModel : BaseModel<AnnouncementBookmark>, IAnnouncementBookmarkRepository
    {
        public IQueryable<Announcement> ReadAnnouncementsByUser(int userId)
        {
            return from a in Context.Set<AnnouncementBookmark>()
                .Include("Announcement")
                .Include("UserProfile")
                   where a.UserId.Equals(userId)
                   orderby a.Id descending
                   select a.Announcement;
        }

        public bool IsIsset(int userId, int announcementId)
        {
            return Read().Any(a => a.AnnouncementId.Equals(announcementId) && a.UserId.Equals(userId));
        }

        public AnnouncementBookmark Read(int userId, int announcementId)
        {
            return Context.Set<AnnouncementBookmark>()
                .Include("Announcement")
                .Include("UserProfile")
                .Single(a => a.AnnouncementId.Equals(announcementId) && a.UserId.Equals(userId));
        }
    }
}