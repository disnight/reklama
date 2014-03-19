using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entity.Shared;
using Domain.Entity.Announcements;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Articles
{
    public class Article: SearchProviderEntity
    {
        [Display(Name = "Строка URL")]
        public string Slug { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ViewsCount { get; set; }

        [Display(Name = "Изображение")]
        public string Logo { get; set; }

        [Required]
        [Display(Name = "Подраздел")]
        [HiddenInput(DisplayValue = false)]
        public int SubsectionId { get; set; }


        [ForeignKey("SubsectionId")]
        public virtual ArticleSubsection Subsection { get; set; }
        public virtual ICollection<ArticleComment> Comments { get; set; }
    }
}
