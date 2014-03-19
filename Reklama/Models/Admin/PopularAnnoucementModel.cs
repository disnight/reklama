using Domain.Repository.Admin;
using Domain.Entity.Admin;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Admin
{
    public class PopularAnnoucementModel: BaseModel<PopularAnnouncement>, IPopularAnnouncementRepository
    {
    }
}