using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entity.Shared;

namespace Domain.Entity.Articles
{
    public class ArticleComment: BaseEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int ArticleId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Минимальная длина поля - 3 симовола, максимальная - 1000")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}
