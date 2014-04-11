using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;
using Reklama.Models.Shared;
using WebGrease.Css.Extensions;

namespace Reklama.Models.Announcements
{
    public class AnnouncementImageModel : BaseModel<AnnouncementImage>, IAnnouncementImageRepository
    {
        public IQueryable<AnnouncementImage> ReadByAnnouncement(int announcementId)
        {
            return Context.Set<AnnouncementImage>().Include("Announcement").Where(a => a.AnnouncementId == announcementId).AsQueryable();
        }

        public void SaveManyImages(int announcementId, string imageNamesSeparated)
        {
            if (imageNamesSeparated != null)
            {
                var images = new Dictionary<string, bool>();
                imageNamesSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ForEach(q => images.Add(q.Split(';')[0], (q.Split(';').Length > 1) && q.Split(';')[1] == "true"));
                var issetImages = from i in ReadByAnnouncement(announcementId) select i.Link;
                var imagesSet = new HashSet<string>(issetImages);

                foreach (var image in images)
                {
                    var obj = new AnnouncementImage
                    {
                        CreatedAt = DateTime.Now,
                        Link = image.Key,
                        IsTitular = image.Value,
                        AnnouncementId = announcementId
                    };

                    InsertOrUpdate(obj);
                }
                
                var imagesToDeleteLink = imagesSet.Except(images.Select(q => q.Key)).ToArray();
                DeleteImages(announcementId, imagesToDeleteLink);
            }
        }


        public void DeleteImages(int announcementId, string[] images)
        {
            foreach(var image in images)
            {
                var announcementImage = FilterOne(i => i.Link.Equals(image));
                Delete(announcementImage);
            }
        }

        public void DeleteImage(int announcementId, string image)
        {
            var imageWithoutPath = image.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
            var announcementImage =
                Context.Set<AnnouncementImage>().Where(a => a.AnnouncementId == announcementId).First(
                    a => a.Link.Equals(imageWithoutPath));
            Delete(announcementImage);
        }

        public void InsertOrUpdate(AnnouncementImage image)
        {
            var obj = Context.Set<AnnouncementImage>().Include("Announcement").FirstOrDefault(a => a.AnnouncementId == image.AnnouncementId && a.Link == image.Link);
            if (obj != null)
            {
                obj.IsTitular = image.IsTitular;
                Save(obj);
            }
            else
            {
                Save(image);
            }
        }
    }
}