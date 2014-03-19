using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Other;

namespace Reklama.Models.Mappers
{
    public class PageConfiguration: EntityTypeConfiguration<Page>
    {
        public PageConfiguration()
        {
            ToTable(TableMap.PageTable);
            HasKey(p => p.Id);

            Property(p => p.Name).HasMaxLength(128).IsRequired().IsUnicode();
            Property(p => p.Description).HasMaxLength(255000).IsRequired().IsUnicode();
            Property(p => p.CreatedAt).IsRequired();
            Property(p => p.IsActive).IsRequired();
            Property(p => p.PageTemplateId).IsOptional();
            Property(p => p.Slug).IsRequired().HasMaxLength(200).IsUnicode();
        }
    }
}