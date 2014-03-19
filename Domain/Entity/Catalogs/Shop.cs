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
    public class Shop: BaseEntity
    {
        public int UserId { get; set; }
        public double Debt { get; set; }
        public string ImageLogo { get; set; }
        public bool IsActive { get; set; }

        //Work days
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        [Display(Name = "Город")]
        public int CityId { get; set; }


        [StringLength(255, ErrorMessage = "Длина поля 'Сайт' не должна быть меньше 4 и больше 255 символов", MinimumLength = 4)]
        [Display(Name = "Сайт")]
        public string Site { get; set; }

        [StringLength(10, ErrorMessage = "Длина поля 'ICQ' не должна быть меньше 5 и больше 10 символов", MinimumLength = 5)]
        [Display(Name = "ICQ")]
        public string Icq { get; set; }

        [StringLength(32, ErrorMessage = "Длина поля 'Skype' не должна быть меньше 4 и больше 32 символов", MinimumLength = 4)]
        [Display(Name = "Skype")]
        public string Skype { get; set; }

        [AllowHtml]
        [StringLength(64, ErrorMessage = "Длина поля 'Название магазина' не должна быть меньше 2 и больше 64 символов", MinimumLength = 2)]
        [Display(Name = "Название магазина")]
        [Required(ErrorMessage = "Поле 'Название магазина' обязательно для заполнения")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Длина поля 'Контактный телефон' не должна быть меньше 5 и больше 255 символов", MinimumLength = 5)]
        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessage = "Поле 'Контактный телефон' обязательно для заполнения")]
        public string Phone { get; set; }

        [AllowHtml]
        [StringLength(128, ErrorMessage = "Длина поля 'Фамилия Имя Отчество' не должна быть меньше 4 и больше 128 символов", MinimumLength = 4)]
        [Display(Name = "Фамилия Имя Отчество")]
        [Required(ErrorMessage = "Поле 'Фамилия Имя Отчество' обязательно для заполнения")]
        public string FullName { get; set; }

        [AllowHtml]
        [StringLength(128, ErrorMessage = "Длина поля 'Название организации' не должна быть меньше 4 и больше 128 символов", MinimumLength = 4)]
        [Display(Name = "Название организации")]
        [Required(ErrorMessage = "Поле 'Название организации' обязательно для заполнения")]
        public string CompanyName { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Длина поля 'Реквизиты' не должна быть меньше 4 и больше 1000 символов", MinimumLength = 4)]
        [Display(Name = "Реквизиты")]
        [Required(ErrorMessage = "Поле 'Реквизиты' обязательно для заполнения")]
        public string Requisites { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Длина поля 'Описание' не должна быть меньше 4 и больше 1000 символов", MinimumLength = 4)]
        [Display(Name = "Описание")]
        public string Description { get; set; }


        [ForeignKey("UserId")]
        public virtual UserProfile User { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual ICollection<ShopComment> Comments { get; set; }
    }
}
