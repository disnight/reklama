using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using Domain.Repository.Realty;
using Reklama.Models.Shared;

namespace Reklama.Models.Realty
{
    public class RealtyPhotoModel: BaseModel<RealtyPhoto>, IRealtyPhotoRepository
    {
        public IQueryable<RealtyPhoto> ReadByRealty(int realtyId)
        {
            return Context.Set<RealtyPhoto>().Include("Realty").Where(a => a.RealtyId == realtyId).AsQueryable();
        }

        public void SaveManyImages(int realtyId, string imageNamesSeparated)
        {
            if (imageNamesSeparated != null)
            {
                var images = imageNamesSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var issetImages = from i in ReadByRealty(realtyId) select i.Link;
                var imagesSet = new HashSet<string>(issetImages);

                foreach (var image in images)
                {
                    if (imagesSet.Contains(image)) continue;

                    Save(new RealtyPhoto
                    {
                        RealtyId = realtyId,
                        CreatedAt = DateTime.Now,
                        Link = image
                    });
                }

                var imagesToDeleteLink = imagesSet.Except(images).ToArray();
                DeleteImages(realtyId, imagesToDeleteLink);
            }
        }


        public void DeleteImages(int realtyId, string[] images)
        {
            foreach (var image in images)
            {
                var realtyImage = FilterOne(i => i.Link.Equals(image));
                Delete(realtyImage);
            }
        }

        public void DeleteImage(int realtyId, string image)
        {
            var imageWithoutPath = image.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
            var realtyImage =
                Context.Set<RealtyPhoto>().Where(a => a.RealtyId == realtyId).First(
                    a => a.Link.Equals(imageWithoutPath));
            Delete(realtyImage);
        }
    }
}