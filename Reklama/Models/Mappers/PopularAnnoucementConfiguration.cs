using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class PopularAnnoucementConfiguration : EntityTypeConfiguration<PopularAnnouncement>
    {
        public PopularAnnoucementConfiguration()
        {
            ToTable(TableMap.PopularAnnoucementTable);
            HasKey(p => p.Id);
            Property(p => p.AnnouncementId).IsRequired();
        }
    }
}