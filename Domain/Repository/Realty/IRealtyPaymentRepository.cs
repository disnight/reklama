using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository.Shared;
using Domain.Entity.Realty;

namespace Domain.Repository.Realty
{
    public interface IRealtyPaymentRepository: IRepository<RealtyPayment>
    {
        IQueryable<RealtyPayment> ReadByUser(int userId);
        IQueryable<RealtyPayment> ReadByRealty(int realtyId);
    }
}
