using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Models.Shared;
using Domain.Enums;

namespace Reklama.Models.Catalog
{
    public class ProductModel: BaseModel<Product>, IProductRepository
    {
        public IQueryable<Product> ReadBySecondCategory(int secondCategoryId)
        {
            return ReadActivesQuery().Where(p => p.SecondCategoryId == secondCategoryId);
        }

        public IQueryable<Product> ReadByThirdCategory(int thirdCategoryId)
        {
            return ReadActivesQuery().Where(p => p.ThirdCategoryId == thirdCategoryId);
        }

        public IQueryable<Product> ReadActivesQuery()
        {
            return Context.Set<Product>().Where(p => p.IsActive == true);
        }


        public IQueryable<Product> Sort(IQueryable<Product> products, SortOrderParams sortOrder, SortOptionsParams sortOptions, 
            int secondCategoryId, int? thirdCategoryId)
        {

            if (thirdCategoryId.HasValue)
            {
                products = products.Where(p => p.ThirdCategoryId == thirdCategoryId.Value);
            }

            if (sortOrder == SortOrderParams.Ascending)
            {
                if (sortOptions == SortOptionsParams.ByName)
                {
                    products = products.OrderBy(p => p.Name);
                }
                else
                {
                    products = products.OrderBy(p => p.ShopProductRefs.Min(spr => spr.Price));
                }
            }
            // Sorting by descending
            else
            {
                if (sortOptions == SortOptionsParams.ByName)
                {
                    products = products.OrderByDescending(p => p.Name);
                }
                else
                {
                    products = products.OrderByDescending(p => p.ShopProductRefs.Max(spr => spr.Price));
                }
            }

            return products;
        }
    }
}