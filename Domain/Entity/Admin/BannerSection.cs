using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;

namespace Domain.Entity.Admin
{
    public class BannerSection: BaseEntity
    {
        public int BannerId { get; set; }
        public int SectionId { get; set; }
    }
}