using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Entity.Shared;
using Domain.Repository.Shared;
using Reklama.Data.Entities;
using Reklama.Models;

namespace Reklama.ViewModels.Shops
{
    public class ShopRegistrationDataViewModel
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ICityRepository _cityRepository;

        public ShopRegistrationDataViewModel()
        {
            var rc = new ReklamaContext();
            _profileRepository = DependencyResolver.Current.GetService<IProfileRepository>();
            _cityRepository = DependencyResolver.Current.GetService<ICityRepository>();
            _cityRepository.Context = rc;
            _profileRepository.Context = rc;
        }

        public Shop Shop { get; set; }
        public decimal MonthlyFee { get; set; }

        public IEnumerable<City> Cities
        {
            get { return _cityRepository.Read(); }
        }

        public string ChangeRegistrationDataHelp { get; set; }

        private City _currentCity;

        public City CurrentCity
        {
            get
            {
                if (!Shop.CityID.HasValue) return null;
                return _currentCity ?? (_currentCity = _cityRepository.Read(Shop.CityID.Value));
            }
        }

        private UserProfile _user;
        public UserProfile User
        {
            get
            {
                return _user ?? (_user = _profileRepository.Read(Shop.UserID.Value));
            }

        }
    }
}