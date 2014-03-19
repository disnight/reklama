using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;
using Reklama.Models.Shared;

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
                var images = imageNamesSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var issetImages = from i in ReadByAnnouncement(announcementId) select i.Link;
                var imagesSet = new HashSet<string>(issetImages);

                foreach (var image in images)
                {
                    if (imagesSet.Contains(image)) continue;

                    Save(new AnnouncementImage
                    {
                        AnnouncementId = announcementId,
                        CreatedAt = DateTime.Now,
                        Link = image
                    });
                }
                
                var imagesToDeleteLink = imagesSet.Except(images).ToArray();
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
    }
}