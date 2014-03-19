using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Announcements;
using Domain.Repository.Shared;

namespace Domain.Repository.Announcements
{
    public interface IAnnouncementBookmarkRepository: IRepository<AnnouncementBookmark>
    {
        IQueryable<Announcement> ReadAnnouncementsByUser(int userId);
        bool IsIsset(int userId, int announcementId);
        AnnouncementBookmark Read(int userId, int announcementId);
    }
}
