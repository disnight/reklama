using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Announcements;
using Domain.Repository.Shared;

namespace Domain.Repository.Announcements
{
    public interface IAnnouncementImageRepository: IRepository<AnnouncementImage>
    {
        IQueryable<AnnouncementImage> ReadByAnnouncement(int announcementId);
        void SaveManyImages(int announcementId, string imageNamesSeparated);
        void DeleteImages(int announcementId, string[] images);
        void DeleteImage(int announcementId, string image);
        void InsertOrUpdate(AnnouncementImage image);
    }
}
