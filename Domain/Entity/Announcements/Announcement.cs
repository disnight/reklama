using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entity.Shared;

namespace Domain.Entity.Announcements
{
    public class Announcement: SearchProviderEntity
    {
        [HiddenInput(DisplayValue = false)]
        public DateTime UpTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime ExpiredAt { get; set; }

        [Display(Name = "Торг")]
        public bool IsAuction { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Должно быть введено вещественное число")]
        [Display(Name = "Цена")]
		[Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:c2}")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Выберите раздел")]
        [Display(Name = "Раздел")]
        public int SectionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ViewsCount { get; set; }

        [Required(ErrorMessage = "Выберите подраздел")]
        [Display(Name = "Подраздел")]
        public int SubsectionId { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [Display(Name = "Город")]
        public int? CityId { get; set; }

        [Display(Name = "Валюта")]
        public int CurrencyId { get; set; }

        [Display(Name = "Показывать мой номер телефона")]
        public bool IsDisplayPhone { get; set; }

        [Display(Name = "Телефон")]
        [StringLength(32, ErrorMessage = "Максимальная длина поля - 32 символа")]
        public string Phone { get; set; }


        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }

        [ForeignKey("SubsectionId")]
        public virtual Subsection Subsection { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile User { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }

        public virtual ICollection<AnnouncementImage> Images { get; set; }
        public virtual ICollection<AnnouncementComment> Comments { get; set; }
    }
}
