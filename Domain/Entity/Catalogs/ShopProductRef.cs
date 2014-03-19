using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Catalogs
{
    public class ShopProductRef: BaseEntity
    {
        public int ShopId { get; set; }
        public int ProductId { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "цена")]
        public decimal Price { get; set; }

        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
