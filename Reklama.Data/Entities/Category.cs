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
    
    public partial class Category
    {
        public Category()
        {
            this.CategoryParametersSection = new HashSet<CategoryParametersSection>();
            this.Product = new HashSet<Product>();
        }
    
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public bool IsPopular { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual ICollection<CategoryParametersSection> CategoryParametersSection { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
