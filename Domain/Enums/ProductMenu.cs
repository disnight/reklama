using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ProductMenu
    {
        [Description("Характеристики")]
        Characteristics,

        [Description("Фото")]
        Photo,

        [Description("Обзор")]
        Review,

        [Description("Отзывы")]
        Feedback,

        [Description("Магазины")]
        Shops
    }
}
