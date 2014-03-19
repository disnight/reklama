using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;

namespace Domain.Entity.Admin
{
    public class Banner: BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsActive { get; set; }
        public string LinkFlash { get; set; }
        public string LinkImage { get; set; }
        public string Url { get; set; }
    }
}