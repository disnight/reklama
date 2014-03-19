using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Announcements;
using Domain.Repository.Shared;

namespace Domain.Repository.Announcements
{
    public interface IAnnouncementCommentRepository: IRepository<AnnouncementComment>
    {
        IEnumerable<AnnouncementComment> ReadByAnnouncement(int announcementId);
        IEnumerable<AnnouncementComment> ReadByUser(int userId);
    }
}
