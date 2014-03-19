using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Shared;
using Domain.Enums;
using Domain.Repository.Shared;
using Domain.Entity.Realty;
using Domain.Repository.Realty;

namespace Reklama.Models.ViewModels.Realty
{
    public class RealtySortByParams
    {
        [Display(Name = "Город")]
        public int? CityId { get; set; }

        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }

        [DataType(DataType.Currency, ErrorMessage="Неверно указана цена")]
        [Range(0, double.MaxValue)]
        [Display(Name = "Начальная цена")]
        public decimal? FromPrice { get; set; }
        
        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency, ErrorMessage = "Неверно указана цена")]
        [Display(Name = "Конечная цена")]
        public decimal? ToPrice { get; set; }

        [Display(Name = "Улица")]
        [StringLength(24, ErrorMessage = "Длина поля 'Улица' не должна быть меньше 4 и больше 128 символов", MinimumLength = 4)]
        public string Street { get; set; }

        //[Display(Name = "Количество комнат")]
        //public List<int> CountsRoom { get; set; }
        
        [Display(Name = "Количество комнат")]
        public List<bool> CountsRoom { get; set; }

        [Range(0, 16000)]
        [Display(Name = "Начальная площадь")]
        public double? FromSquare { get; set; }

        [Range(0, 16000)]
        [Display(Name = "Конечная площадь")]
        public double? ToSquare { get; set; }

        [Range(1, 255)]
        [Display(Name = "Начальный этаж")]
        public int? FromFloor { get; set; }

        [Range(1,255)]
        [Display(Name = "Конечный этаж")]
        public int? ToFloor { get; set; }

        [Range(1, 255)]
        [Display(Name = "Минимальное количество этажей")]
        public int? FromFloorCount { get; set; }

        [Range(1, 255)]
        [Display(Name = "Максимальное количество этажей")]
        public int? ToFloorCount { get; set; }

        [Range(0, 32)]
        [Display(Name = "Начальная высота потолков")]
        public double? FromCeillingHeight { get; set; }

        [Range(0, 32)]
        [Display(Name = "Конечная высота полотолков")]
        public double? ToCeillingHeight { get; set; }

        [Display(Name = "С пристройкой")]
        public bool WithExtension { get; set; }

        [Display(Name = "С гаражом")]
        public bool WithGarage { get; set; }

        [Display(Name = "С подвалом")]
        public bool WithBasement { get; set; }

        [Display(Name = "С огородом")]
        public bool WithGarden { get; set; }

        [Display(Name = "С фотографиями")]
        public bool WithPhoto { get; set; }

        [Display(Name = "Частные объявления")]
        public bool IsPerson { get; set; }

        [Display(Name = "С возможностью торга")]
        public bool IsAuction { get; set; }

        public bool IsEnableSort { get; set; }

        public SortOrderParams SortOrder { get; set; }
        public SortOptionsParams SortOptions { get; set; }
        public int? SectionId { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }

        public string StringCountsRoom { 
            get
            {
                string stringCountsRoom = String.Empty;

                for (int i = 0; i < CountsRoom.Count; i++)
                {
                    stringCountsRoom += "CountsRoom[" + i + "]" + CountsRoom[i].ToString() + "&";
                }
                return stringCountsRoom;
            } 
        }

        public virtual SelectList Cities { get; set; }
        public virtual SelectList Categories { get; set; }

        public RealtySortByParams()
        {
            IList<City> cities;
            IList<RealtyCategory> categories;
            SortOptions = SortOptionsParams.ByDate;
            SortOrder = SortOrderParams.Descending;
            CurrentPage = 1;
            PageSize = ProjectConfiguration.Get.ItemsOnPage[0];
            //Initialize list of counts room
            if(CountsRoom == null)
                CountsRoom = new List<bool>() { false, false, false, false, false, false };
            var cityRepository = DependencyResolver.Current.GetService<ICityRepository>();
            var categoryRepository = DependencyResolver.Current.GetService<IRealtyCategoryRepository>();
            using (var rc = new ReklamaContext())
            {
                cityRepository.Context = rc;
                cities = cityRepository.Read().OrderBy(c => c.Id).ToList();

                categoryRepository.Context = rc;
                categories = categoryRepository.Read().OrderBy(c => c.Id).ToList();
            }
            PopulateCity(cities);
            PopulateCategory(categories);
        }

        private void PopulateCategory(IList<RealtyCategory> categories, object selectedCategory = null)
        {
            categories.Insert(0, new RealtyCategory() { Name = "Все", Id = 0 });
            Categories = new SelectList(categories, "Id", "Name", selectedCategory);
        }

        private void PopulateCity(IList<City> cities, object selectedCity = null)
        {
            cities.Insert(0, new City() { Name = "Все", Id = 0 });
            Cities = new SelectList(cities, "Id", "Name", selectedCity);
        }

    }
}