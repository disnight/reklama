using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Catalogs;
using Domain.Repository.Shared;

namespace Domain.Repository.Catalogs
{
    public interface IProductSectionParamRefRepository: IRepository<ProductSectionParamRef>
    {
        IEnumerable<ProductSectionParamRef> ReadBySection(int sectionId);
        IQueryable<ProductParam> ReadParamsBySection(int sectionId);
    }
}
