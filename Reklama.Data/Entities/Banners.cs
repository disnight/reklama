//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Reklama.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Banners
    {
        public int ID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int BannerType { get; set; }
        public Nullable<int> ImageID { get; set; }
        public Nullable<int> CountOfClicks { get; set; }
        public string Comments { get; set; }
        public bool IsShow { get; set; }
        public string Link { get; set; }
    
        public virtual BannerTypes BannerTypes { get; set; }
        public virtual Images Images { get; set; }
    }
}