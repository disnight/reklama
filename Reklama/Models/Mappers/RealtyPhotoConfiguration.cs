using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Domain.Entity.Realty;

namespace Reklama.Models.Mappers
{
    public class RealtyPhotoConfiguration: EntityTypeConfiguration<RealtyPhoto>
    {
        public RealtyPhotoConfiguration()
        {
            ToTable(TableMap.RealtyPhotoTable);
            HasKey(r => r.Id);
            Property(r => r.CreatedAt).IsRequired();
            Property(r => r.Link).IsRequired().HasMaxLength(255);
            Property(r => r.RealtyId).IsRequired();
            Property(a => a.IsTitular).IsOptional();
        }
    }
}