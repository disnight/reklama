using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Repository.Admin;
using Domain.Entity.Admin;
using Reklama.Models.Shared;

namespace Reklama.Models.Admin
{
    public class BannerModel: BaseModel<Banner>, IBannerRepository
    {
        public IQueryable<Banner> GetImageBanners(bool isActive)
        {
            return Context.Set<Banner>().Where(b => b.LinkImage != null && b.IsActive == true);
        }

        public IQueryable<Banner> GetFlashBanners(bool isActive)
        {
            return Context.Set<Banner>().Where(b => b.LinkFlash != null && b.IsActive == true);
        }
    }
}