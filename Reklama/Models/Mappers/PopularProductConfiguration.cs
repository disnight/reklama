using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class PopularProductConfiguration: EntityTypeConfiguration<PopularProduct>
    {
        public PopularProductConfiguration()
        {
            ToTable(TableMap.PopularProductTable);
            HasKey(p => p.Id);
            Property(p => p.ProductId).IsRequired();
        }
    }
}