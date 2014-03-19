using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Realty;
using Domain.Repository.Shared;

namespace Domain.Repository.Realty
{
    public interface IRealtyCommentRepository: IRepository<RealtyComment>
    {
        IQueryable<RealtyComment> ReadByUser(int userId);
        IQueryable<RealtyComment> ReadByRealty(int realtyId);
    }
}
