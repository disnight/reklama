using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entity.Shared;

namespace Domain.Entity.Catalogs
{
    public class ProductFeedback: BaseEntity
    {
        [AllowHtml]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Минимальная длина поля - 3 символа, максимальная - 1000")]
        public string Comment { get; set; }

        [Required]
        [HiddenInput]
        public DateTime CreatedAt { get; set; }

        [Required]
        [HiddenInput]
        public int ProductId { get; set; }

        [Required]
        [HiddenInput]
        public int UserId { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile User { get; set; }
    }
}
