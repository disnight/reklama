using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using Domain.Repository.Realty;
using Reklama.Models.Shared;
using WebGrease.Css.Extensions;

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
                var images = new Dictionary<string, bool>();
                imageNamesSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ForEach(q => images.Add(q.Split(';')[0], (q.Split(';').Length > 1) && q.Split(';')[1] == "true"));
                var issetImages = from i in ReadByRealty(realtyId) select i.Link;
                var imagesSet = new HashSet<string>(issetImages);

                foreach (var image in images)
                {
                    var obj = new RealtyPhoto
                    {
                        CreatedAt = DateTime.Now,
                        Link = image.Key,
                        IsTitular = image.Value,
                        RealtyId = realtyId
                    };
                    InsertOrUpdate(obj);
                }

                var imagesToDeleteLink = imagesSet.Except(images.Select(q => q.Key)).ToArray();
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

        public void InsertOrUpdate(RealtyPhoto image)
        {
            var obj = Context.Set<RealtyPhoto>().Include("Realty").FirstOrDefault(a => a.RealtyId == image.RealtyId && a.Link == image.Link);
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