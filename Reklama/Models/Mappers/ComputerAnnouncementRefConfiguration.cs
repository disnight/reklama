using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Common;
using Domain.Entity.Other;

namespace Reklama.Models.Mappers
{
    public class ComputerAnnouncementRefConfiguration : EntityTypeConfiguration<ComputerAnnouncementRef>
    {
        public ComputerAnnouncementRefConfiguration()
        {
            ToTable(TableMap.ComputerAnnouncementRefTable);
            HasKey(p => p.Id);

            Property(p => p.ComputerId).IsRequired();
            Property(p => p.AnnouncementsId).IsRequired();
        }
    }
}