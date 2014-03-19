using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class ShopConfiguration : EntityTypeConfiguration<Shop>
    {
        public ShopConfiguration()
        {
            ToTable(TableMap.ShopTable);
            HasKey(s => s.Id);
            Property(s => s.UserId).IsRequired();
            Property(s => s.IsActive).IsRequired();
            Property(s => s.Debt).IsRequired();
            Property(s => s.ImageLogo).HasMaxLength(255).IsOptional().IsUnicode();
            Property(s => s.CityId).IsRequired();
            Property(s => s.Site).HasMaxLength(255).IsOptional().IsUnicode();
            Property(s => s.Name).HasMaxLength(64).IsRequired().IsUnicode();
            Property(s => s.Phone).HasMaxLength(255).IsRequired().IsUnicode();
            Property(s => s.FullName).HasMaxLength(128).IsRequired().IsUnicode();
            Property(s => s.CompanyName).HasMaxLength(128).IsRequired().IsUnicode();
            Property(s => s.Description).HasMaxLength(1000).IsOptional().IsUnicode();
            Property(s => s.Requisites).HasMaxLength(1000).IsRequired().IsUnicode();
            Property(s => s.Monday).IsRequired();
            Property(s => s.Tuesday).IsRequired();
            Property(s => s.Wednesday).IsRequired();
            Property(s => s.Thursday).IsRequired();
            Property(s => s.Friday).IsRequired();
            Property(s => s.Saturday).IsRequired();
            Property(s => s.Sunday).IsRequired();
            Property(s => s.Icq).HasMaxLength(10).IsUnicode();
            Property(s => s.Skype).HasMaxLength(32).IsUnicode();
        }
    }
}