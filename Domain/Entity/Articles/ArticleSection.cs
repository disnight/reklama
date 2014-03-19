using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Articles
{
    public class ArticleSection: BaseEntity
    {
        [Required]
        [StringLength(64, ErrorMessage = "Минимальная длина - 3, максимальная - 64 символа", MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<ArticleSubsection> Subsections { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
