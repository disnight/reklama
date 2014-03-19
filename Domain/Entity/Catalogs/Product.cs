using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Catalogs
{
    public class Product: SearchProviderEntity
    {
        [Required]
        [Display(Name = "Категория товара")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Главное изображение")]
        public string Image { get; set; }

        [Display(Name = "Категория 2 уровня товара")]
        public int? SecondCategoryId { get; set; }

        [Display(Name = "Категория 3 уровня товара")]
        public int? ThirdCategoryId { get; set; }

        [Display(Name = "Ссылка на обзор")]
        [StringLength(255, ErrorMessage = "Максимальная длина поля - 255 символов")]
        public string ReviewLink { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CatalogCategory Category { get; set; }

        [ForeignKey("SecondCategoryId")]
        public virtual CatalogSecondCategory SecondCategory { get; set; }

        [ForeignKey("ThirdCategoryId")]
        public virtual CatalogThirdCategory ThirdCategory { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }

        public virtual ICollection<ProductFeedback> Feedbacks { get; set; }

        public virtual ICollection<ShopProductRef> ShopProductRefs { get; set; }
    }
}
