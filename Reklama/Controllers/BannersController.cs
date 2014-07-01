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
            if(banner == null) return EmptyResult();

            return PartialView("Banners/_UnderFilters", new BannerViewModel
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

    }
}
