using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Shared;
using Domain.Enums;
using Domain.Repository.Shared;
using Reklama.Models.Shared;
using Reklama.Models.SortModels;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;

namespace Reklama.Models.ViewModels.Announcement
{
    public class FilterParams
    {
        [Display(Name = "Город")]
        public int? CityId { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Неверно указана цена")]
        [Display(Name = "от")]
        public decimal? PriceStart { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Неверно указана цена")]
        [Display(Name = "до")]
        public decimal? PriceEnd { get; set; }

        [Required]
        [Display(Name = "Объявления с фотографиями")]
        public bool HasPhoto { get; set; }

        [Required]
        [Display(Name = "Только с возможностью торга")]
        public bool HasAuction { get; set; }

        public bool IsEnabledFilter { get; set; }

        public virtual SelectList Cities { get; set; }


        public SortOrderParams SortOrder { get; set; }
        public SortOptionsParams SortOptions { get; set; }
        public int? SectionId { get; set; }
        public int? SubsectionId { get; set; }
        public int? CategoryId { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }


        public FilterParams()
        {
            IList<City> cities;
            SortOptions = SortOptionsParams.ByDate;
            SortOrder = SortOrderParams.Descending;
            CurrentPage = 1;
            PageSize = ProjectConfiguration.Get.ItemsOnPage[0];
            var cityRepository = DependencyResolver.Current.GetService<ICityRepository>();
            using (var rc = new ReklamaContext())
            {
                cityRepository.Context = rc;
                cities = cityRepository.Read().OrderBy(c => c.Id).ToList();
            }
            PopulateCity(cities);
        }

        public FilterParams(IList<City> cities, object selectedCity = null)
        {
            PopulateCity(cities, selectedCity);
        }

        private void PopulateCity(IList<City> cities, object selectedCity = null)
        {
            cities.Insert(0, new City() { Name = "Все", Id = 0 });
            Cities = new SelectList(cities, "Id", "Name", selectedCity);
        }

        public Section GetSection()
        {
            Section section = null;

            if (!SectionId.HasValue)
            {
                return null;
            }

            using (var rc = new ReklamaContext())
            {
                var sectionRepository = DependencyResolver.Current.GetService<ISectionRepository>();
                sectionRepository.Context = rc;
                section = sectionRepository.Read(SectionId.Value);
            }
            return section;
        }

        public Subsection GetSubsection()
        {
            Subsection subsection = null;

            if (!SubsectionId.HasValue)
            {
                return null;
            }

            using (var rc = new ReklamaContext())
            {
                var subsectionRepository = DependencyResolver.Current.GetService<ISubsectionRepository>();
                subsectionRepository.Context = rc;
                subsection = subsectionRepository.Read(SubsectionId.Value);
            }

            return subsection;
        }
    }
}