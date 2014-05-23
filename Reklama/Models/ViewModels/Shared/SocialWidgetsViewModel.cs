using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.ViewModels.Shared
{
    public class SocialWidgetsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageLink { get; set; }
        public bool IsInline { get; set; }
    }
}