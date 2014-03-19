using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Enums;
using Domain.Repository.Admin;
using Reklama.Models;
using Domain.Entity.Admin;
using Domain.Repository.Realty;
using Domain.Repository.Announcements;
using Domain.Entity.Announcements;
using Reklama.Models.ViewModels.Announcement;
using PagedList;

namespace Reklama.Controllers
{
    public class HomeController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IPopularProductRepository _popularProductRepository;
        private readonly INewSectionInCatalogRepository _newSectionInCatalogRepository;
        private readonly IPopularSectionInCatalogRepository _popularSectionCatalogRepository;
        private readonly IPopularAnnouncementRepository _popularAnnoucementRepository;
        private readonly IRealtyRepository _realtyRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IPopularSectionInCatalogRepository popularSectionInCatalogRepository,
                            IPopularAnnouncementRepository popularAnnoucementRepository,
                            INewSectionInCatalogRepository newSectionInCatalogRepository,
                            IPopularProductRepository popularProductRepository,
                            IRealtyRepository realtyRepository,
                            IAnnouncementRepository announcementRepository,
                            ICategoryRepository categoryRepository)
        {
            _popularSectionCatalogRepository = popularSectionInCatalogRepository;
            _popularSectionCatalogRepository.Context = rc;

            _newSectionInCatalogRepository = newSectionInCatalogRepository;
            _newSectionInCatalogRepository.Context = rc;

            _popularProductRepository = popularProductRepository;
            _popularProductRepository.Context = rc;

            ViewBag.Slogan = ProjectConfiguration.Get.AnnouncementConfig.Slogan;
            ViewBag.SelectedSiteCategory = CategorySearch.Announcement;

            _popularAnnoucementRepository = popularAnnoucementRepository;
            _popularAnnoucementRepository.Context = rc;

            _realtyRepository = realtyRepository;
            _realtyRepository.Context = rc;

            _announcementRepository = announcementRepository;
            _announcementRepository.Context = rc;

            _categoryRepository = categoryRepository;
            _categoryRepository.Context = rc;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }


        [ChildActionOnly]
        public ActionResult PopularSectionsInCatalog()
        {
            var popularSections = _popularSectionCatalogRepository.Read();
            return View(popularSections);
        }

        [ChildActionOnly]
        public ActionResult NewInCatalog()
        {
            var news = _newSectionInCatalogRepository.Read();
            return View(news);
        }

        [ChildActionOnly]
        public ActionResult PopularProducts()
        {
            var products = _popularProductRepository.Read().ToList();
            List<PopularProduct> result = new List<PopularProduct>();
            if (products.Count() <= 5)
            {
                result = products.ToList();
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    int index = new Random().Next(products.Count());
                    result.Add(products[index]);
                    products.RemoveAt(index);
                }
            }
            return View(result);
        }

        [ChildActionOnly]
        public ActionResult ArticlesAndReviews()
        {
            return View();
        }


        public ActionResult RestrictedAccess()
        {
            return View(Request.UrlReferrer);
        }

        [ChildActionOnly]
        public ActionResult PopularAnnouncement()
        {
            var popularAnnouncements = _popularAnnoucementRepository.Read().ToList();
            List<PopularAnnouncement> result = new List<PopularAnnouncement>();
            if(popularAnnouncements.Count() <= 5)
            {
                result = popularAnnouncements;
            }
            else
            {
                for(int i = 0; i < 5; i++)
                {
                    int index = new Random().Next(popularAnnouncements.Count());
                    result.Add(popularAnnouncements[index]);
                    popularAnnouncements.RemoveAt(index);
                }
            }
            return View(result);
        }

        public ActionResult RealtyBlock()
        {
            var realty = _realtyRepository.Read().OrderByDescending(x => x.CreatedAt).ToList();
            if(realty.Count < 4)
            {
                return View(realty);
            }
            return View(realty.GetRange(0, 4));
        }

        public ActionResult AnnouncementBlock()
        {
            var announcements = _announcementRepository.Read().OrderByDescending(x => x.CreatedAt).Where(x => x.SectionId != 3).ToList();
            if (announcements.Count < 4)
            {
                return View(announcements);
            }
            return View(announcements.GetRange(0, 4));
        }

        public ActionResult AutoBlock()
        {
            var auto = _announcementRepository.Read().OrderByDescending(x => x.CreatedAt).Where(x => x.SectionId == 3).ToList();
            if(auto.Count < 4)
            {
                return View(auto);
            }
            return View(auto.GetRange(0, 4));
        }

        public ActionResult AnnouncementList(FilterParams filterParams = null)
        {
            var popularAnnouncements = _popularAnnoucementRepository.Read();
            List<Announcement> announcements = new List<Announcement>();
            foreach(var popularAnnouncement in popularAnnouncements)
            {
                announcements.Add(popularAnnouncement.Announcement);
            }
            var announcementsToSort = _announcementRepository.Sort(announcements.AsQueryable(), filterParams.SortOrder, filterParams.SortOptions, filterParams.SectionId,
                                     filterParams.SubsectionId, filterParams.CategoryId);
            ViewBag.Categories = _categoryRepository.Read(); 
            ViewBag.FilterParams = filterParams;
            return View("AnnouncementList", announcementsToSort.ToPagedList((filterParams.CurrentPage.HasValue) ? filterParams.CurrentPage.Value : 1, filterParams.PageSize));
        }

    }
}
