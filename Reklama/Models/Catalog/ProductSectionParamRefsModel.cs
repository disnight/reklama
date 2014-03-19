using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;

namespace Reklama.Models.Catalog
{
    public class ProductSectionParamRefsModel : BaseModel<ProductSectionParamRef>, IProductSectionParamRefRepository
    {
        public override IQueryable<ProductSectionParamRef> Read()
        {
            return base.Read().OrderBy(r => r.CategoryId);
        }

        public IEnumerable<ProductSectionParamRef> ReadBySection(int sectionId)
        {
            return base.Read().Where(r => r.CategoryId == sectionId).OrderBy(r => r.Param.Name);
        }

        public IQueryable<ProductParam> ReadParamsBySection(int sectionId)
        {
            return (from r in ReadBySection(sectionId) select r.Param).AsQueryable();
        }
    }
}