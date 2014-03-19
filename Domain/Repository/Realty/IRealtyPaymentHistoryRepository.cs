using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Realty;
using Domain.Repository.Shared;

namespace Domain.Repository.Realty
{
    public interface IRealtyPaymentHistoryRepository: IRepository<RealtyPaymentHistory>
    {
        IQueryable<RealtyPaymentHistory> ReadByUser(int userId);
        IQueryable<RealtyPaymentHistory> ReadByRealty(int realtyId);
    }
}
