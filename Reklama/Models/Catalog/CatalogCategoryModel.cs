using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;

namespace Reklama.Models.Catalog
{
    public class CatalogCategoryModel: BaseModel<CatalogCategory>, ICatalogCategoryRepository
    {
    }
}