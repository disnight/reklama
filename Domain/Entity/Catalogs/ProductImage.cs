using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Catalogs
{
    public class ProductImage: BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Link { get; set; }

        [Required]
        public int ProductId { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
