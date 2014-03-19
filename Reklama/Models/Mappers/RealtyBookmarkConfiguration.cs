using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class RealtyBookmarkConfiguration: EntityTypeConfiguration<RealtyBookmark>
    {
        public RealtyBookmarkConfiguration()
        {
            ToTable(TableMap.RealtyBookmarkTable);
            HasKey(r => r.Id);
            Property(r => r.RealtyId).IsRequired();
            Property(r => r.UserId).IsRequired();
        }
    }
}