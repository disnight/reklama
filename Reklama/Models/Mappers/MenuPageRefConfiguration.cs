using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Other;

namespace Reklama.Models.Mappers
{
    public class MenuPageRefConfiguration: EntityTypeConfiguration<MenuPageRef>
    {
        public MenuPageRefConfiguration()
        {
            ToTable(TableMap.MenuPageRefTable);
            HasKey(m => m.Id);

            Property(m => m.MenuId).IsRequired();
            Property(m => m.PageId).IsRequired();
            Property(m => m.Priority).IsRequired();
        }
    }
}