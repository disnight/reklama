using Domain.Entity.Shared;
using Domain.Repository.Shared;
using PagedList;
using Reklama.Data.Entities;
using Reklama.ViewModels.Catalog;

namespace Reklama.ViewModels.Shops
{
    public class ShopDetailsPageViewModel
    {
        private readonly IProfileRepository _profileRepository;

        public ShopDetailsPageViewModel(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
            Comments = new PagedList<FeedbackViewModel>(null, 1, 1);
        }

        public Shop Shop { get; set; }
        public IPagedList<FeedbackViewModel> Comments { get; set; }

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