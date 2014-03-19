using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Catalogs
{
    public class ShopCategoryRef: BaseEntity
    {
        public int ShopId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CatalogSecondCategory Category { get; set; }
    }
}
