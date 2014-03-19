using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using Domain.Repository.Realty;
using Reklama.Models.Shared;

namespace Reklama.Models.Realty
{
    public class RealtyPaymentHistoryModel: BaseModel<RealtyPaymentHistory>, IRealtyPaymentHistoryRepository
    {
        public IQueryable<RealtyPaymentHistory> ReadByUser(int userId)
        {
            return Context.Set<RealtyPaymentHistory>().Where(r => r.UserId == userId);
        }

        public IQueryable<RealtyPaymentHistory> ReadByRealty(int realtyId)
        {
            return Context.Set<RealtyPaymentHistory>().Where(r => r.RealtyId == realtyId);
        }
    }
}