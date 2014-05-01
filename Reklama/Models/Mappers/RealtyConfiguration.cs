using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class RealtyConfiguration: EntityTypeConfiguration<Domain.Entity.Realty.Realty>
    {
        public RealtyConfiguration()
        {
            ToTable(TableMap.RealtyTable);
            HasKey(r => r.Id);
            Property(r => r.CategoryId).IsRequired();
            Property(r => r.CeillingHeight).IsOptional();
            Property(r => r.CityId).IsRequired();
            Property(r => r.CreatedAt).IsRequired();
            Property(r => r.Description).IsRequired().HasMaxLength(100000).IsUnicode();
            Property(r => r.ExpiredAt).IsOptional();
            Property(r => r.Floor).IsOptional();
            Property(r => r.FloorCount).IsOptional();
            Property(r => r.IsActive).IsRequired();
            Property(r => r.IsAuction).IsRequired();
            Property(r => r.Name).IsRequired().HasMaxLength(180).IsUnicode();
            Property(r => r.Price).IsOptional();
            Property(r => r.CurrencyId).IsRequired();
            Property(r => r.RoomsCount).IsOptional();
            Property(r => r.SectionId).IsRequired();
            Property(r => r.SmallDescription).IsRequired().HasMaxLength(600).IsUnicode();
            Property(r => r.Square).IsOptional();
            Property(r => r.Street).IsOptional().HasMaxLength(128).IsUnicode();
            Property(r => r.IsAgency).IsRequired();
            Property(r => r.UserId).IsRequired();
            Property(r => r.Views).IsRequired();
            Property(r => r.WithBasement).IsRequired();
            Property(r => r.WithExtension).IsRequired();
            Property(r => r.WithGarage).IsRequired();
            Property(r => r.WithGarden).IsRequired();
            Property(r => r.Phone).HasMaxLength(32).IsUnicode();
            Property(r => r.IsDisplayPhone).IsRequired();
            Property(r => r.ForDays).IsOptional();
            Property(r => r.AgencyName).IsOptional().HasMaxLength(64).IsUnicode();
            Property(r => r.UpTime).IsRequired();
            Property(a => a.ContactName).IsOptional();
            Property(a => a.ContactEmail).IsOptional();

            HasRequired(r => r.UserProfile);
        }
    }
}