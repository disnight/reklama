using Domain.Entity.Catalogs;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public class ShopProductRefConfiguration : EntityTypeConfiguration<ShopProductRef>
    {
        public ShopProductRefConfiguration()
        {
            ToTable(TableMap.ShopProductTable);
            HasKey(x => x.Id);

            Property(x => x.Price).IsRequired();
            Property(x => x.ProductId).IsRequired();
            Property(x => x.ShopId).IsRequired();
        }
    }
}