using Domain.Entity.Catalogs;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public class ProductBookmarkConfiguration: EntityTypeConfiguration<ProductBookmark>
    {
        public ProductBookmarkConfiguration()
        {
            ToTable(TableMap.ProductBookmarkTable);
            HasKey(p => p.Id);

            Property(p => p.ProductId).IsRequired();
            Property(p => p.UserId).IsRequired();
        }
    }
}