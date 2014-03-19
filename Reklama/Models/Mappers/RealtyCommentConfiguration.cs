using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class RealtyCommentConfiguration: EntityTypeConfiguration<RealtyComment>
    {
        public RealtyCommentConfiguration()
        {
            ToTable(TableMap.RealtyCommentTable);
            HasKey(r => r.Id);
            Property(r => r.Comment).IsRequired().HasMaxLength(1000).IsUnicode();
            Property(r => r.CreatedAt).IsRequired();
            Property(r => r.RealtyId).IsRequired();
            Property(r => r.UserId).IsRequired();
            Property(r => r.IsActive).IsRequired();
        }
    }
}