using System;
using Domain.Entity.Shared;
using Domain.Repository.Shared;
using Reklama.Models.Shared;

namespace Reklama.ViewModels.Catalog
{
    public class FeedbackViewModel
    {
        private readonly IProfileRepository _profileRepository;

        public FeedbackViewModel(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public long ID { get; set; }

        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserID { get; set; }

        private UserProfile _user;

        public UserProfile User
        {
            get { return _user ?? (_user = _profileRepository.Read(UserID)); }
        }
    }
}