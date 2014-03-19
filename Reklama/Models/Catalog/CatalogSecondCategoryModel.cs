using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;

namespace Reklama.Models.Catalog
{
    public class CatalogSecondCategoryModel: BaseModel<CatalogSecondCategory>, ICatalogSecondCategoryRepository
    {
        public IQueryable<CatalogSecondCategory> ReadByCategory(int categoryId)
        {
            return ((ReklamaContext) Context).CatalogSecondCategories.Where(c => c.CategoryId == categoryId);
        }
    }
}