using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using Domain.Repository.Realty;
using Reklama.Models.Shared;

namespace Reklama.Models.Realty
{
    public class RealtyBookmarkModel: BaseModel<RealtyBookmark>, IRealtyBookmarkRepository
    {
        public IQueryable<RealtyBookmark> ReadByUser(int userId)
        {
            return Context.Set<RealtyBookmark>().Where(r => r.UserId == userId);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadRealtyByUser(int userId)
        {
            return from a in Context.Set<RealtyBookmark>()
                .Include("Realty")
                .Include("UserProfile")
                   where a.UserId.Equals(userId)
                   orderby a.Id descending
                   select a.Realty;
        }

        public bool IsIsset(int userId, int realtyId)
        {
            return Read().Any(a => a.RealtyId.Equals(realtyId) && a.UserId.Equals(userId));
        }

        public RealtyBookmark Read(int userId, int realtyId)
        {
            return Context.Set<RealtyBookmark>()
                .Include("Realty")
                .Include("UserProfile")
                .Single(a => a.RealtyId.Equals(realtyId) && a.UserId.Equals(userId));
        }
    }
}