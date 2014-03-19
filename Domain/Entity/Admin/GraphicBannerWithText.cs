using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;

namespace Domain.Entity.Admin
{
    public class GraphicBannerWithText: BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
    }
}