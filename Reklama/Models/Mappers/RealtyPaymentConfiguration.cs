using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Domain.Entity.Realty;

namespace Reklama.Models.Mappers
{
    public class RealtyPaymentConfiguration: EntityTypeConfiguration<RealtyPayment>
    {
        public RealtyPaymentConfiguration()
        {
            ToTable(TableMap.RealtyPaymentTable);
            HasKey(r => r.Id);
            Property(r => r.Cost).IsRequired();
            Property(r => r.CreatedAt).IsRequired();
            Property(r => r.RealtyId).IsRequired();
            Property(r => r.TransactionId).IsRequired();
            Property(r => r.UserId).IsRequired();
        }
    }
}