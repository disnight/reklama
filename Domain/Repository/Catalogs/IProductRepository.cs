using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Catalogs;
using Domain.Repository.Shared;
using Domain.Enums;

namespace Domain.Repository.Catalogs
{
    public interface IProductRepository: IRepository<Product>
    {
        IQueryable<Product> ReadBySecondCategory(int secondCategoryId);
        IQueryable<Product> ReadByThirdCategory(int thirdCategoryId);
        IQueryable<Product> ReadActivesQuery();
        IQueryable<Product> Sort(IQueryable<Product> products, SortOrderParams sortOrder, SortOptionsParams sortOptions, int secondCategoryId, int? thirdCategoryId);
    }
}
