using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class BannerSectionConfiguration: EntityTypeConfiguration<BannerSection>
    {
        public BannerSectionConfiguration()
        {
            ToTable(TableMap.BannerSectionTable);
            HasKey(b => b.Id);
            Property(b => b.BannerId).IsRequired();
        }
    }
}