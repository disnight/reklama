using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class ShopCategoryRefConfiguration : EntityTypeConfiguration<ShopCategoryRef>
    {
        public ShopCategoryRefConfiguration()
        {
            ToTable(TableMap.ShopCategoryTable);
            HasKey(x => x.Id);
            Property(x => x.ShopId).IsRequired();
            Property(x => x.CategoryId).IsRequired();
        }
    }
}