using Domain.Entity.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public class PaymentConfiguration: EntityTypeConfiguration<Payment>
    {
        public PaymentConfiguration()
        {
            ToTable(TableMap.PaymentTable);
            HasKey(p => p.Id);

            Property(p => p.AdId).IsRequired();
            Property(p => p.Cost).IsRequired();
            Property(p => p.CreatedAt).IsRequired();
            Property(p => p.PaysystemInvId).IsRequired();
            Property(p => p.SiteCategory).IsRequired();
            Property(p => p.Status).IsRequired();
        }
    }
}