using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Catalogs;
using Domain.Repository.Shared;

namespace Domain.Repository.Catalogs
{
    public interface IProductSubsectionParamRefRepository: IRepository<ProductSubsectionParamRef>
    {
        IEnumerable<ProductSubsectionParamRef> ReadBySubsection(int subsectionId);
        IQueryable<ProductParam> ReadParamsBySubsection(int subsectionId);
    }
}
