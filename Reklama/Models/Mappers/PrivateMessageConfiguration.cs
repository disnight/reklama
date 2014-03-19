using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;

namespace Reklama.Models.Mappers
{
    public class PrivateMessageConfiguration: EntityTypeConfiguration<PrivateMessage>
    {
        public PrivateMessageConfiguration()
        {
            this.HasKey(m => m.Id);
            Property(m => m.IsRead).IsRequired();
            Property(m => m.Message).IsUnicode().IsRequired().HasMaxLength(2000);
            Property(m => m.RecepientId).IsRequired();
            Property(m => m.SenderId).IsRequired();
            Property(m => m.Subject).IsOptional().IsUnicode().HasMaxLength(128);
            Property(m => m.CreatedAt).IsRequired();

            HasRequired(m => m.Sender);
            HasRequired(m => m.Recepient);
        }
    }
}