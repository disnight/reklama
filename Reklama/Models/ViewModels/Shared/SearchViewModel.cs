using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Shared;
using Domain.Enums;
using Reklama.Models.ViewModels.Announcement;

namespace Reklama.Models.ViewModels.Shared
{
    public class SearchViewModel: FilterParams
    {
        [Display(Name = "Название")]
        [DataType(DataType.Text)]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Минимальная длина - 3 символа, максимальная - 200")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DefaultValue(CategorySearch.Announcement)]
        public CategorySearch Category { get; set; }

        [Display(Name = "Искать только в названиях")]
        public bool OnlyByName { get; set; }

        public SearchViewModel(): base() { }

        public SearchViewModel(IList<City> cities, object selectedCity = null) : base(cities, selectedCity) { }
    }
}