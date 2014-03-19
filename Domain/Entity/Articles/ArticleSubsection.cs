using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Articles
{
    public class ArticleSubsection: BaseEntity
    {
        [Required(ErrorMessage = "Выберите подраздел")]
        [StringLength(64, ErrorMessage = "Минимальная длина - 3, максимальная - 64 символа", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        public bool IsNew { get; set; }


        public virtual ArticleSection Section { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
