using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Enums;

namespace Reklama.Models.SortModels
{
    /// <summary>
    /// Представляет идентификатор объявления из какой либо категории<br />
    /// (Объявления, Недвижимость, Авто, ...)
    /// </summary>
    public class CategorySearchSortModel
    {
        public int Id { get; set; }
        public CategorySearch Category { get; set; }
    }
}