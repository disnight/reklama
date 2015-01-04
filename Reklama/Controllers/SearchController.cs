using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Announcements;
using Domain.Enums;
using Domain.Repository.Announcements;
using Domain.Repository.Shared;
using Reklama.Data.Servises;
using Reklama.Models.SortModels;
using Reklama.Models.ViewModels.Announcement;
using Reklama.Models.ViewModels.Shared;
using PagedList;
using Reklama.Models.Shared;
using Domain.Entity.Articles;
using Domain.Repository.Articles;
using Reklama.Models;
using Domain.Repository.Realty;
using Domain.Entity.Realty;
using Reklama.Models.ViewModels.Realty;
using Domain.Repository.Catalogs;
using Domain.Entity.Catalogs;

namespace Reklama.Controllers
{
    public class SearchController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly ProductService _productService = new ProductService();

        private IArticleRepository _articleRepository;
        private IAnnouncementRepository _announcementRepository;
        private IRealtyRepository _realtyRepository;
        private readonly IRealtyCategoryRepository _realtyCategoryRepository;
        private readonly IRealtySectionRepository _realtySectionRepository;
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public SearchController(IArticleRepository articleRepository, IAnnouncementRepository announcementRepository,
            ISubsectionRepository subsectionRepository, ISectionRepository sectionRepository, ICategoryRepository categoryRepository, IRealtyRepository realtyRepository, 
            IRealtyCategoryRepository realtyCategoryRepository, IRealtySectionRepository realtySectionRepository, IProductRepository productRepository)
        {
            _articleRepository = articleRepository;
            _articleRepository.Context = rc;

            _announcementRepository = announcementRepository;
            _announcementRepository.Context = rc;

            _subsectionRepository = subsectionRepository;
            _subsectionRepository.Context = rc;

            _sectionRepository = sectionRepository;
            _sectionRepository.Context = rc;

            _categoryRepository = categoryRepository;
            _categoryRepository.Context = rc;

            _realtyRepository = realtyRepository;
            _realtyRepository.Context = rc;

            _realtyCategoryRepository = realtyCategoryRepository;
            _realtyCategoryRepository.Context = rc;

            _realtySectionRepository = realtySectionRepository;
            _realtySectionRepository.Context = rc;

            _productRepository = productRepository;
            _productRepository.Context = rc;
        }

        public ActionResult Search(SearchViewModel searchViewModel)
        {
            InitializeListData(searchViewModel.Category);

            if (searchViewModel.Name == null)
            {
                return Redirect("/");
            }

            searchViewModel.Name = searchViewModel.Name.Trim();

            if (searchViewModel.Name.Length < ProjectConfiguration.Get.MinSearchStringLength)
            {
                TempData["error"] = "Необходимо ввести ключевые слова для поиска. Минимальная длина - 3 символа";

                return Redirect("/");
            }


            // Sorting by search categories and retrieving category items
            switch (searchViewModel.Category)
            {
                // Search by announcements
                case CategorySearch.Announcement:
                    var announcements = (new SearchModel<Announcement>(_announcementRepository))
                        .Search(searchViewModel.Name, searchViewModel.OnlyByName)
                        .OrderByDescending(a => a.UpTime)
                        .AsQueryable();

                    if (searchViewModel.IsEnabledFilter)
                    {
                        announcements = _announcementRepository.SortByParams(announcements, searchViewModel.HasPhoto, searchViewModel.HasAuction,
                                                         searchViewModel.CityId, searchViewModel.PriceStart, searchViewModel.PriceEnd);
                    }

                    announcements = _announcementRepository.Sort(announcements, searchViewModel.SortOrder, searchViewModel.SortOptions, searchViewModel.SectionId,
                                                 searchViewModel.SubsectionId, searchViewModel.CategoryId);

                    if (searchViewModel.SectionId.HasValue)
                    {
                        ViewBag.Subsections = _subsectionRepository.ReadBySection(searchViewModel.SectionId.Value);
                    }

                    ViewBag.SearchViewModel = searchViewModel;

                    return View("AnnouncementList",
                        announcements.ToPagedList((searchViewModel.CurrentPage.HasValue) ? searchViewModel.CurrentPage.Value : 1, searchViewModel.PageSize));
                    break;

                //Search by realty
                case CategorySearch.Realty:
                    var realty = (new SearchModel<Realty>(_realtyRepository))
                    .Search(searchViewModel.Name, searchViewModel.OnlyByName)
                    .OrderByDescending(a => a.UpTime)
                    .AsQueryable();

                    realty = _realtyRepository.Sort(realty, searchViewModel.SortOrder, searchViewModel.SortOptions, searchViewModel.SectionId, searchViewModel.CategoryId);

                    ViewBag.SearchViewModel = new SearchRealtyViewModel(searchViewModel.Name, searchViewModel.Category, searchViewModel.OnlyByName);

                    return View("RealtyList",
                        realty.ToPagedList((searchViewModel.CurrentPage.HasValue) ? searchViewModel.CurrentPage.Value : 1, searchViewModel.PageSize));
                    break;

                //Search by articles
                case CategorySearch.Article:
                    var articles = (new SearchModel<Article>(_articleRepository))
                        .Search(searchViewModel.Name, searchViewModel.OnlyByName)
                        .OrderByDescending(a => a.CreatedAt)
                        .AsQueryable();

                    ViewBag.SearchViewModel = searchViewModel;

                    return View("ArticleList",
                        articles.ToPagedList((searchViewModel.CurrentPage.HasValue) ? searchViewModel.CurrentPage.Value : 1, searchViewModel.PageSize));
                    break;


                // Search by product
                default:
                    var products = _productService.Search(searchViewModel.Name, searchViewModel.OnlyByName)
                        .OrderBy(p => p.Title).ToList();
                        
                        //(new SearchModel<Product>(_productRepository))
                        //.Search(searchViewModel.Name, searchViewModel.OnlyByName)
                        //.OrderBy(p => p.Name)
                        //.AsQueryable();

                    ViewBag.SearchViewModel = searchViewModel;

                    return View("ProductList",
                        products.ToPagedList((searchViewModel.CurrentPage.HasValue) ? searchViewModel.CurrentPage.Value : 1, searchViewModel.PageSize));
                    break;
            }

            return HttpNotFound();
        }


        public ActionResult SearchRealty(SearchRealtyViewModel searchViewModel)
        {
            ViewBag.SearchViewModel = searchViewModel;

            InitializeListData(searchViewModel.Category);
            var realty = (new SearchModel<Realty>(_realtyRepository))
                        .Search(searchViewModel.Name, searchViewModel.OnlyByName)
                        .OrderByDescending(a => a.UpTime)
                        .AsQueryable();

            realty = _realtyRepository.Sort(realty, searchViewModel.SortOrder, searchViewModel.SortOptions, searchViewModel.SectionId, searchViewModel.CategoryId);
            realty = realty = _realtyRepository.SortByParams(realty, searchViewModel.CategoryId, searchViewModel.CityId, searchViewModel.FromPrice, searchViewModel.ToPrice, searchViewModel.CountsRoom,
                                                 searchViewModel.FromSquare, searchViewModel.ToSquare, searchViewModel.FromFloorCount, searchViewModel.ToFloorCount, searchViewModel.FromFloor,
                                                 searchViewModel.ToFloor, searchViewModel.FromCeillingHeight, searchViewModel.ToCeillingHeight, searchViewModel.WithPhoto,
                                                 searchViewModel.IsAuction, searchViewModel.IsPerson, searchViewModel.WithGarage, searchViewModel.WithGarden, searchViewModel.WithExtension,
                                                 searchViewModel.WithBasement, searchViewModel.Street);

            return View("RealtyList", realty.ToPagedList((searchViewModel.CurrentPage.HasValue) ? searchViewModel.CurrentPage.Value : 1, searchViewModel.PageSize));
        }


        private void InitializeListData(CategorySearch category)
        {
            switch (category)
            {
                case CategorySearch.Announcement:
                    ViewBag.Sections = _sectionRepository.Read();
                    ViewBag.Categories = _categoryRepository.Read();
                    break;
                case CategorySearch.Realty:
                    ViewBag.Sections = _realtySectionRepository.Read();
                    ViewBag.Categories = _realtyCategoryRepository.Read();
                    break;
            }
            
        }



        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
