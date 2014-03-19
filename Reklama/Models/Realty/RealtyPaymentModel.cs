using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Repository.Realty;
using Domain.Entity.Realty;
using Reklama.Models.Shared;

namespace Reklama.Models.Realty
{
    public class RealtyPaymentModel: BaseModel<RealtyPayment>, IRealtyPaymentRepository
    {
        public IQueryable<RealtyPayment> ReadByUser(int userId)
        {
            return Context.Set<RealtyPayment>().Where(r => r.UserId == userId);
        }

        public IQueryable<RealtyPayment> ReadByRealty(int realtyId)
        {
            return Context.Set<RealtyPayment>().Where(r => r.RealtyId == realtyId);
        }
    }
}