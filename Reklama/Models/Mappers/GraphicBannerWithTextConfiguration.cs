using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class GraphicBannerWithTextConfiguration: EntityTypeConfiguration<GraphicBannerWithText>
    {
        public GraphicBannerWithTextConfiguration()
        {
            ToTable(TableMap.GraphicBannerWithTextTable);
            HasKey(g => g.Id);
            Property(g => g.CreatedAt).IsRequired();
            Property(g => g.Image).IsRequired().HasMaxLength(255);
            Property(g => g.Link).IsRequired().HasMaxLength(255);
            Property(g => g.Name).IsRequired().HasMaxLength(255);
        }
    }
}