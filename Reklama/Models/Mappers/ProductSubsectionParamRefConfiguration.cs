using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class ProductSubsectionParamRefConfiguration : EntityTypeConfiguration<ProductSubsectionParamRef>
    {
        public ProductSubsectionParamRefConfiguration()
        {
            ToTable(TableMap.ProductSubsectionParamRefTable);
            HasKey(p => p.Id);

            Property(p => p.ParamId).IsRequired();
            Property(p => p.SubsectionId).IsRequired();
        }
    }
}