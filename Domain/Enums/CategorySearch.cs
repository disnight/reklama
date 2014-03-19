using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum CategorySearch
    {
        [Description("Объявления")]
        Announcement,

        [Description("Недвижимость")]
        Realty,

        [Description("Товары")]
        Product,

        [Description("Статьи")]
        Article,

        [Description("Главная")]
        Index,

        [Description("Авто")]
        Auto
    }
}
