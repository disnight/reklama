using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entity.Shared;

namespace Domain.Entity.Admin
{
    public class CatalogConfig: BaseEntity
    {
        [Display(Name = "Текстовый инфоблок раздела")]
        [StringLength(255, ErrorMessage = "Максимальная длина - 255")]
        public string Slogan { get; set; }

        [AllowHtml]
        [Display(Name = "Промо-текст")]
        [StringLength(1000, ErrorMessage = "Максимальная длина - 255")]
        public string PromoText { get; set; }

        [AllowHtml]
        [Display(Name = "Текст уведомления при отображении магазинов товара")]
        [StringLength(1000, ErrorMessage = "Максимальная длина - 255")]
        public string WarningText { get; set; }

        [AllowHtml]
        [Display(Name = "Промо-текст при регистрации магазина")]
        [StringLength(1000, ErrorMessage = "Максимальная длина - 255")]
        public string RegShopPromoText { get; set; }
    }
}
