using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class ProductParamConfiguration: EntityTypeConfiguration<ProductParam>
    {
        public ProductParamConfiguration()
        {
            ToTable(TableMap.ProductParamTable);
            HasKey(p => p.Id);

            Property(p => p.Name).IsUnicode().IsRequired().HasMaxLength(128);
        }
    }
}