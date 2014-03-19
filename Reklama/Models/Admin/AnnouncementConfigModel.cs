using Domain.Entity.Admin;
using Domain.Repository.Admin;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Admin
{
    public class AnnouncementConfigModel: BaseModel<AnnouncementConfig>, IAnnouncementConfigRepository
    {
        public override IQueryable<AnnouncementConfig> Read()
        {
            return ((ReklamaContext)Context).AnnouncementConfigs
                .Include("Premium1")
                .Include("Premium2")
                .Include("Premium3");
        }

        public AnnouncementConfig ReadConfig()
        {
            return Read().First();
        }
    }
}