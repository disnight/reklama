using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;

namespace Reklama.Models.Catalog
{
    public class ProductSubsectionParamRefsModel : BaseModel<ProductSubsectionParamRef>, IProductSubsectionParamRefRepository
    {
        public override IQueryable<ProductSubsectionParamRef> Read()
        {
            return base.Read().OrderBy(r => r.SubsectionId);
        }

        public IEnumerable<ProductSubsectionParamRef> ReadBySubsection(int subsectionId)
        {
            return base.Read().Where(r => r.SubsectionId == subsectionId).OrderBy(r => r.Param.Name);
        }

        public IQueryable<ProductParam> ReadParamsBySubsection(int subsectionId)
        {
            return (from r in ReadBySubsection(subsectionId) select r.Param).AsQueryable();
        }
    }
}