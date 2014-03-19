using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Entity.Shared;

namespace Domain.Entity.Admin
{
    public class PopularProduct: BaseEntity
    {
        public int ProductId { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}