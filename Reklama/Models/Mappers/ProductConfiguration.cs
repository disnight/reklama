using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class ProductConfiguration: EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable(TableMap.ProductTable);
            HasKey(p => p.Id);

            Property(p => p.CategoryId).IsRequired();
            Property(p => p.SecondCategoryId).IsOptional();
            Property(p => p.ThirdCategoryId).IsOptional();
            Property(p => p.ReviewLink).HasMaxLength(255).IsUnicode();
        }
    }
}