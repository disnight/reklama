using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;

namespace Reklama.Models.Catalog
{
    public class CatalogThirdCategoryModel: BaseModel<CatalogThirdCategory>, ICatalogThirdCategoryRepository
    {
        public IQueryable<CatalogThirdCategory> ReadBySecondCategory(int category2Id)
        {
            return ((ReklamaContext) Context).CatalogThirdCategories.Where(a => a.SecondCategoryId == category2Id);
        }
    }
}