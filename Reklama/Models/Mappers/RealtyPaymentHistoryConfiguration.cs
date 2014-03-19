using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Domain.Entity.Realty;

namespace Reklama.Models.Mappers
{
    public class RealtyPaymentHistoryConfiguration: EntityTypeConfiguration<RealtyPaymentHistory>
    {
        public RealtyPaymentHistoryConfiguration()
        {
            ToTable(TableMap.RealtyPaymentHistoryTable);
            HasKey(r => r.Id);
            Property(r => r.ComplectedAt).IsRequired();
            Property(r => r.Cost).IsRequired();
            Property(r => r.CreatedAt).IsRequired();
            Property(r => r.ErrorMessage).IsRequired().HasMaxLength(255);
            Property(r => r.IsSuccess).IsRequired();
            Property(r => r.PaysystemTransactionId).IsRequired().HasMaxLength(255);
            Property(r => r.RealtyId).IsRequired();
            Property(r => r.TransactionId).IsRequired();
            Property(r => r.UserId).IsRequired();
        }
    }
}