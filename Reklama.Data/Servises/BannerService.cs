using System.Collections.Generic;
using System.Linq;
using Reklama.Data.Base;
using Reklama.Data.Entities;

namespace Reklama.Data.Servises
{
    public class BannerService
    {
        private readonly IRepository<Banners> _banneRepository;
        public BannerService()
        {
            _banneRepository = new Repository<Banners>(new ReklamaCustomEntities());
        }
        public Banners GetBanner(string controller, string action, Domain.Enums.BannerTypes type)
        {
            return
                _banneRepository.Find(
                    q => q.Controller == controller && q.Action == action && q.BannerType == (int) type && q.IsShow)
                    .FirstOrDefault();
        }

        public bool SaveBanner(Banners banner)
        {
            _banneRepository.Edit(banner);
            _banneRepository.SaveChanges();
            return true;
        }

        public Banners GetBanner(int id)
        {
            return _banneRepository.Find(q => q.ID == id).FirstOrDefault();
        }

        public IEnumerable<Banners> GetAll()
        {
            return
                _banneRepository.GetAll();
        }
    }
}