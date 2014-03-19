using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Admin;

namespace Reklama.Models.Mappers
{
    public class CatalogConfigConfiguration: EntityTypeConfiguration<CatalogConfig>
    {
        public CatalogConfigConfiguration()
        {
            ToTable(TableMap.CatalogConfigTable);
            HasKey(t => t.Id);

            Property(c => c.PromoText).HasMaxLength(255).IsOptional().IsUnicode();
            Property(c => c.RegShopPromoText).HasMaxLength(1000).IsOptional().IsUnicode();
            Property(c => c.Slogan).HasMaxLength(1000).IsOptional().IsUnicode();
            Property(c => c.WarningText).HasMaxLength(1000).IsOptional().IsUnicode();
        }
    }
}
