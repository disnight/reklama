using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository.Shared;
using Reklama.Data.Base;
using Reklama.Data.Entities;
using Reklama.Data.Servises;
using Reklama.Models;
using Reklama.Models.ViewModels.Banners;
using BannerTypes = Domain.Enums.BannerTypes;

namespace Reklama.Controllers
{
    public class BannersController : Controller
    {
        private string ParentController
        {
            get
            {
                return ControllerContext.ParentActionViewContext.RouteData.Values["Controller"] as string;
            }
        }

        private string ParentAction
        {
            get
            {
                return ControllerContext.ParentActionViewContext.RouteData.Values["Action"] as string;
            }
        }

        //
        // GET: /Under Filters Banner/

        private readonly BannerService _bannerService = new BannerService();

        public ActionResult GetUnderFiltersBanner()
        {
            var banner = _bannerService.GetBanner(ParentController, ParentAction, BannerTypes.UnderFilter);
            if(banner == null || banner.Images == null) return EmptyResult();

            return PartialView("Banners/_UnderFilters", new BannerViewModel
            {
                ImageUrl = banner.Images.ImagePath,
                Link = banner.Link,
                Target = "_blank"
            });
        }


        public ActionResult GetTopBanner()
        {
            var banner = _bannerService.GetBanner(ParentController, ParentAction, BannerTypes.Top);
            if (banner == null || banner.Images == null) return EmptyResult();

            return PartialView("Banners/_TopBanner", new BannerViewModel
            {
                ImageUrl = banner.Images.ImagePath,
                Link = banner.Link,
                Target = "_blank"
            });
        }

        public ActionResult GetMain1Banner()
        {
            var banner = _bannerService.GetBanner("Home", "Home", BannerTypes.Main1);
            if (banner == null || banner.Images == null) return EmptyResult();

            return PartialView("Banners/_Main1Banner", new BannerViewModel
            {
                ImageUrl = banner.Images.ImagePath,
                Link = banner.Link,
                Target = "_blank"
            });
        }

        public ActionResult GetMain2Banner()
        {
            var banner = _bannerService.GetBanner(ParentController, ParentAction, BannerTypes.Main2);
            if (banner == null || banner.Images == null)
            {
                var banner3 = _bannerService.GetBanner(ParentController, ParentAction, BannerTypes.Main3);
                if (banner3 == null || banner3.Images == null) return EmptyResult();
                return Empty23Result();
            }

            return PartialView("Banners/_Main23Banner", new BannerViewModel
            {
                ImageUrl = banner.Images.ImagePath,
                Link = banner.Link,
                Target = "_blank"
            });
        }

        public ActionResult GetMain3Banner()
        {
            var banner = _bannerService.GetBanner(ParentController, ParentAction, BannerTypes.Main3);
            if (banner == null || banner.Images == null)
            {
                var banner2 = _bannerService.GetBanner(ParentController, ParentAction, BannerTypes.Main2);
                if (banner2 == null || banner2.Images == null) return EmptyResult();
                return Empty23Result();
            }

            return PartialView("Banners/_Main23Banner", new BannerViewModel
            {
                ImageUrl = banner.Images.ImagePath,
                Link = banner.Link,
                Target = "_blank"
            });
        }

        private ActionResult EmptyResult()
        {
            return PartialView("Banners/_EmptyBanner");
        }


        private ActionResult Empty23Result()
        {
            return PartialView("Banners/_EmptyMain23Banner");
        }
    }
}
