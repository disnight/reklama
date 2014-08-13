using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Reklama.Data.Entities;

namespace Reklama.Data.Servises
{
    public static class SortService
    {
        public static IEnumerable<Product> Sort(this IEnumerable<Product> products, SortOrderParams sortOrder, SortOptionsParams sortOptions,
           int secondCategoryId, int? thirdCategoryId)
        {

            if (thirdCategoryId.HasValue)
            {
                products = products.Where(p => p.ManufacturerID == thirdCategoryId.Value);
            }

            if (sortOrder == SortOrderParams.Ascending)
            {
                products = sortOptions == SortOptionsParams.ByName ? products.OrderBy(p => p.Title) : products.OrderBy(p => p.ShopProduct.Min(spr => spr.Price));
            }
            // Sorting by descending
            else
            {
                products = sortOptions == SortOptionsParams.ByName ? products.OrderByDescending(p => p.Title) : products.OrderByDescending(p => p.ShopProduct.Max(spr => spr.Price));
            }

            return products;
        }

        public static IEnumerable<ShopProduct> Sort(this IEnumerable<ShopProduct> shops, SortOrderParams sortOrder, SortOptionsParams sortOptions, bool byShop = true)
        {

            if (sortOrder == SortOrderParams.Ascending)
            {
                shops = sortOptions == SortOptionsParams.ByName ? byShop ? shops.OrderBy(p => p.Shop.Title) : shops.OrderBy(p => p.Product.Title) : shops.OrderBy(p => p.Price);
            }
            else
            {
                shops = sortOptions == SortOptionsParams.ByName ? byShop ? shops.OrderByDescending(p => p.Shop.Title) : shops.OrderByDescending(p => p.Product.Title) : shops.OrderByDescending(p => p.Price);
            }

            return shops;
        }
    }
}