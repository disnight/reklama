using System.Web.Mvc;
using Domain.Entity.Shared;
using Domain.Repository.Shared;
using PagedList;
using Reklama.Models;
using Reklama.ViewModels.Catalog;

namespace Reklama.ViewModels.Shops
{
    public class ShopPageViewModel
    {
        private readonly IProfileRepository _profileRepository;

        public ShopPageViewModel()
        {
            _profileRepository = DependencyResolver.Current.GetService<IProfileRepository>();
            _profileRepository.Context = new ReklamaContext();
        }

        public Data.Entities.Shop Shop { get; set; }
        public decimal MonthlyFee { get; set; }

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