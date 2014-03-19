using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Repository.Realty;
using Domain.Entity.Realty;
using Reklama.Models.Shared;

namespace Reklama.Models.Realty
{
    public class RealtyFeedbackModel: BaseModel<RealtyComment>, IRealtyCommentRepository
    {
        public IQueryable<RealtyComment> ReadByUser(int userId)
        {
            return Context.Set<RealtyComment>().Where(r => r.UserId == userId);
        }

        public IQueryable<RealtyComment> ReadByRealty(int realtyId)
        {
            return Context.Set<RealtyComment>().Where(r => r.RealtyId == realtyId);
        }
    }
}