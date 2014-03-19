using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entity.Catalogs
{
    public class ShopComment : BaseEntity
    {
        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Минимальная длина поля - 3 символа, максимальная - 1000")]
        public string Comment { get; set; }

        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ShopId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsActive { get; set; }

        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }
    }
}
