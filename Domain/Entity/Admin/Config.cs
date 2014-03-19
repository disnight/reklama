using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Admin
{
    public class Config: BaseEntity
    {
        public string Description { get; set; }
        public bool ? IsEnable { get; set; }
        public string Name { get; set; }

        [AllowHtml]
        [StringLength(1000)]
        public string Value { get; set; }

        public bool IsEnableValue { get; set; }
    }
}