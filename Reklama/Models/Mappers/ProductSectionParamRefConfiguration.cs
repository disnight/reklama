using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class ProductSectionParamRefConfiguration : EntityTypeConfiguration<ProductSectionParamRef>
    {
        public ProductSectionParamRefConfiguration()
        {
            ToTable(TableMap.ProductSectionParamRefTable);
            HasKey(p => p.Id);

            Property(p => p.ParamId).IsRequired();
            Property(p => p.CategoryId).IsRequired();
        }
    }
}