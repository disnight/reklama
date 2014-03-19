using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class ShopCommentConfiguration : EntityTypeConfiguration<ShopComment>
    {
        public ShopCommentConfiguration()
        {
            ToTable(TableMap.ShopCommentTable);
            HasKey(r => r.Id);
            Property(r => r.Comment).IsRequired().HasMaxLength(1000).IsUnicode();
            Property(r => r.CreatedAt).IsRequired();
            Property(r => r.ShopId).IsRequired();
            Property(r => r.UserId).IsRequired();
            Property(r => r.IsActive).IsRequired();
        }
    }
}