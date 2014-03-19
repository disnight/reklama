using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class BannerConfiguration: EntityTypeConfiguration<Banner>
    {
        public BannerConfiguration()
        {
            ToTable(TableMap.BannerTable);
            HasKey(b => b.Id);
            Property(b => b.CreatedAt).IsRequired();
            Property(b => b.ExpiresAt).IsRequired();
            Property(b => b.IsActive).IsRequired();
            Property(b => b.LinkFlash).IsOptional().HasMaxLength(255).IsUnicode();
            Property(b => b.LinkImage).IsOptional().HasMaxLength(255).IsUnicode();
            Property(b => b.Url).IsRequired().HasMaxLength(255).IsUnicode();
        }
    }
}