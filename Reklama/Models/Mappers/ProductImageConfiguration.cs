using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class ProductImageConfiguration: EntityTypeConfiguration<ProductImage>
    {
        public ProductImageConfiguration()
        {
            ToTable(TableMap.ProductImageTable);
            HasKey(p => p.Id);

            Property(p => p.Link).IsRequired().HasMaxLength(255).IsUnicode();
            Property(p => p.ProductId).IsRequired();
        }
    }
}