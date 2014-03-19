using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class ProductParamValueConfiguration: EntityTypeConfiguration<ProductParamValue>
    {
        public ProductParamValueConfiguration()
        {
            ToTable(TableMap.ProductParamValueTable);
            HasKey(p => p.Id);

            Property(p => p.ParamId).IsRequired();
            Property(p => p.ProductId).IsRequired();
            Property(p => p.Value).IsUnicode().IsRequired().HasMaxLength(1000);
        }
    }
}