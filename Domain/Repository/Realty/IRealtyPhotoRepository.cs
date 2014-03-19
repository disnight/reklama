using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository.Shared;
using Domain.Entity.Realty;

namespace Domain.Repository.Realty
{
    public interface IRealtyPhotoRepository: IRepository<RealtyPhoto>
    {
        IQueryable<RealtyPhoto> ReadByRealty(int realtyId);
        void SaveManyImages(int realtyId, string imageNamesSeparated);
        void DeleteImages(int realtyId, string[] images);
        void DeleteImage(int realtyId, string image);
    }
}
