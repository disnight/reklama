using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Catalogs;
using Domain.Repository.Shared;

namespace Domain.Repository.Catalogs
{
    public interface IProductParamRepository: IRepository<ProductParam>
    {
    }
}
