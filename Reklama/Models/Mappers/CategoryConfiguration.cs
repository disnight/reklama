using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;

namespace Reklama.Models.Mappers
{
    public class CategoryConfiguration: EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable(TableMap.CategoryTable);
            HasKey(c => c.Id);
            Property(c => c.Name).HasMaxLength(64).IsRequired().IsUnicode();
        }
    }
}