using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;

namespace Reklama.Models.Mappers
{
    public class ProductFeedbackConfiguration: EntityTypeConfiguration<ProductFeedback>
    {
        public ProductFeedbackConfiguration()
        {
            ToTable(TableMap.ProductFeedbackTable);
            HasKey(f => f.Id);

            Property(f => f.Comment).HasMaxLength(1000).IsUnicode().IsRequired();
            Property(f => f.CreatedAt).IsRequired();
            Property(f => f.ProductId).IsRequired();
            Property(f => f.UserId).IsRequired();
        }
    }
}