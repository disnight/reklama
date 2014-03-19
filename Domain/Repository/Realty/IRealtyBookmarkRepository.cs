using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Realty;
using Domain.Repository.Shared;

namespace Domain.Repository.Realty
{
    public interface IRealtyBookmarkRepository: IRepository<RealtyBookmark>
    {
        IQueryable<RealtyBookmark> ReadByUser(int userId);
        bool IsIsset(int userId, int realtyId);
        RealtyBookmark Read(int userId, int realtyId);
        IQueryable<Domain.Entity.Realty.Realty> ReadRealtyByUser(int userId);
    }
}
