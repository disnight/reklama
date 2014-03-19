using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Realty
{
    public class Realty: SearchProviderEntity
    {
        //public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public DateTime UpTime { get; set; }
        //public bool IsActive { get; set; }
        public bool IsAuction { get; set; }
        [Required(ErrorMessage = "Выберите лицо")]
        [Display(Name = "Лицо")]
        public bool IsAgency { get; set; }
        public int UserId { get; set; }
        public int Views { get; set; }
        public bool WithExtension { get; set; }
        public bool WithGarage { get; set; }
        public bool WithBasement { get; set; }
        public bool WithGarden { get; set; }
        public bool ForDays { get; set; }
        public bool IsDisplayPhone { get; set; }

        [Display(Name = "Телефон")]
        [StringLength(32, ErrorMessage = "Максимальная длина поля - 32 символа")]
        public string Phone { get; set; }

        [Display(Name = "'Высота потолков'")]
        [Range(0, 32)]
        public double? CeillingHeight { get; set; }

        [Display(Name = "'На каком этаже'")]
        [Range(1, 255)]
        public int? Floor { get; set; }

        [Display(Name = "'Этажей в доме'")]
        [Range(1, 255)]
        public int? FloorCount { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "'Цена'")]
        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency, ErrorMessage="Должно быть введено вещественное число")]
        [DisplayFormat(DataFormatString = "{0:c2}")]
        public decimal? Price { get; set; }

        [Display(Name = "'Количество комнат'")]
        public byte? RoomsCount { get; set; }

        [Display(Name = "'Площадь'")]
        [Range(0, 16000)]
        public double? Square { get; set; }

        

        //[StringLength(128, ErrorMessage = "Длина поля 'Заголовок' не должна быть меньше 4 и больше 180 символов", MinimumLength = 4)]
        //[Required(ErrorMessage = "Поле 'Заголовок' обязательно для заполнения")]
        //[Display(Name = "Заголовок")]
        //public string Name { get; set; }

        //[DataType(DataType.MultilineText)]
        //[StringLength(600, ErrorMessage = "Длина поля 'Краткое описание' не должна быть меньше 4 и больше 600 символов", MinimumLength = 4)]
        //[Required(ErrorMessage = "Поле 'Краткое описание' обязательно для заполнения")]
        //[Display(Name = "Краткое описание")]
        //public string SmallDescription { get; set; }

        //[DataType(DataType.MultilineText)]
        //[StringLength(1800, ErrorMessage = "Длина поля 'Описание' не должна быть меньше 4 и больше 1800 символов", MinimumLength = 4)]
        //[Required(ErrorMessage = "Поле 'Описание' обязательно для заполнения")]
        //[Display(Name = "Описание")]
        //public string Description { get; set; }

        [StringLength(128, ErrorMessage = "Длина поля 'Улица' не должна быть меньше 4 и больше 128 символов", MinimumLength = 4)]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [StringLength(64, ErrorMessage = "Длина поля 'Название агенства' не должна быть меньше 4 и больше 64 символов", MinimumLength = 4)]
        [Display(Name = "Название агенства")]
        public string AgencyName { get; set; }

        [Required(ErrorMessage = "Поле 'Категория' обязательно для заполнения")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Поле 'Валюта' обязательно для заполнения")]
        [Display(Name = "Валюта")]
        public int CurrencyId { get; set; }

        [Required(ErrorMessage = "Поле 'Город' обязательно для заполнения")]
        [Display(Name = "Город")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Поле 'Раздел' обязательно для заполнения")]
        [Display(Name = "Раздел")]
        public int SectionId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual RealtyCategory RealtyCategory { get; set; }
        [ForeignKey("SectionId")]
        public virtual RealtySection RealtySection { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<RealtyPhoto> Photos { get; set; }
        public virtual ICollection<RealtyComment> Comments { get; set; }
    }
}
