using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository.Admin;
using Domain.Repository.Catalogs;
using PagedList;
using Reklama.Models;
using PagedList;
using Domain.Entity.Catalogs;
using Reklama.Models.ViewModels.Catalog;
using Domain.Enums;
using WebMatrix.WebData;

namespace Reklama.Controllers
{
    public class ProductController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();
        private readonly IProductRepository _productRepository;
        private readonly IProductParamValueRepository _productParamValueRepository;
        private readonly ICatalogCategoryRepository _catalogCategoryRepository;
        private readonly ICatalogSecondCategoryRepository _catalogSecondCategoryRepository;
        private readonly IProductBookmarkRepository _productBookmarkRepository;
        private readonly IPopularProductRepository _popularProductRepository;

        public ProductController(IProductRepository productRepository, IProductParamValueRepository productParamValueRepository,
            ICatalogCategoryRepository catalogCategoryRepository, ICatalogSecondCategoryRepository catalogSecondCategoryRepository,
            IProductBookmarkRepository productBookmarkRepository, IPopularProductRepository popularProductRepository)
        {
            _productRepository = productRepository;
            _productRepository.Context = rc;

            _productParamValueRepository = productParamValueRepository;
            _productParamValueRepository.Context = rc;

            _catalogCategoryRepository = catalogCategoryRepository;
            _catalogCategoryRepository.Context = rc;

            _catalogSecondCategoryRepository = catalogSecondCategoryRepository;
            _catalogSecondCategoryRepository.Context = rc;

            _productBookmarkRepository = productBookmarkRepository;
            _productBookmarkRepository.Context = rc;

            _popularProductRepository = popularProductRepository;
            _popularProductRepository.Context = rc;

            ViewBag.SelectedSiteCategory = CategorySearch.Product;
            ViewBag.Slogan = ProjectConfiguration.Get.CatalogConfig.Slogan;
        }

        public ActionResult Details(int id)
        {
            var product = _productRepository.Read(id);

            if(product == null)
            {
                return HttpNotFound();
            }

            if (!product.IsActive)
            {
                if (User.IsInRole("Administrator"))
                {
                    TempData["notice"] = "Это неактивное объявление. Его можете видеть только вы";
                }
                else
                {
                    return HttpNotFound();
                }
            }

            ViewBag.ProductParamValues = _productParamValueRepository.ReadByProduct(id).ToArray().ToList();
            ViewBag.IsIssetInBookmark = _productBookmarkRepository.IsIsset(WebSecurity.CurrentUserId, product.Id);
            return View(product);
        }


        public ActionResult Photos(int id)
        {
            var product = _productRepository.Read(id);

            try
            {
                ViewBag.IsIssetInBookmark = _productBookmarkRepository.IsIsset(WebSecurity.CurrentUserId, product.Id);
            }catch{}
            return View(product);
        }


        public ActionResult Feedbacks(int id, int? commentPage)
        {
            var product = _productRepository.Read(id);
            ViewBag.ProductId = product.Id;
            ViewBag.Comments = (product.Feedbacks != null && product.Feedbacks.Count > 0)
                ? product.Feedbacks.ToPagedList(commentPage ?? 1, ProjectConfiguration.Get.CommentsOnPage)
                : null;
            ViewBag.IsIssetInBookmark = _productBookmarkRepository.IsIsset(WebSecurity.CurrentUserId, product.Id);

            return View(product);
        }


        public ActionResult Catalog()
        {
            var categories = _catalogCategoryRepository.Read();
            return View(categories);
        }


        public ActionResult List(ProductsFilterParams filter)
        {
            if (filter == null)
            {
                filter = new ProductsFilterParams();
            }
            var products = _productRepository.ReadBySecondCategory(filter.SecondCategoryId);
            products = _productRepository.Sort(products, filter.SortOrder, filter.SortOptions, filter.SecondCategoryId, filter.ThirdCategoryId);
            ViewBag.SecondCategory = _catalogSecondCategoryRepository.Read(filter.SecondCategoryId);
            ViewBag.Filter = filter;

            return View( products.ToPagedList(filter.Page, filter.PageSize) );
        }

        public ActionResult ProductsShop(ProductsShopFilterParams filter)
        {
            if (!filter.ShopId.HasValue)
            {
                HttpNotFound();
            }
            if (filter == null)
            {
                filter = new ProductsShopFilterParams();
            }
            var products = _productRepository.ReadBySecondCategory(filter.SecondCategoryId);
            products = products.Where(x => x.ShopProductRefs.Where(y => y.ShopId == filter.ShopId).FirstOrDefault() != null);
            var secondCategory = _catalogSecondCategoryRepository.Read(filter.SecondCategoryId);
            products = _productRepository.Sort(products, filter.SortOrder, filter.SortOptions, filter.SecondCategoryId, filter.ThirdCategoryId);
            ViewBag.Filter = filter;
            ViewBag.Name = secondCategory.Name;

            return View(products.ToPagedList(filter.Page, filter.PageSize));
        }

        [Authorize(Roles="Administrator,Moderator")]
        public ActionResult InactiveProductsList()
        {
            var inactiveProducts = _productRepository.Read().Where(p => !p.IsActive).OrderBy(p => p.Name);
            TempData["notice"] = "Ниже отображены НЕАКТИВНЫЕ товары.<br />Их видеть можете только вы!";
            ViewBag.SecondCategory = _catalogSecondCategoryRepository.Read().First();
            ViewBag.Filter = new ProductsFilterParams();
            return View("List",
                        inactiveProducts.ToPagedList(1, int.MaxValue));
        }


        public ActionResult Shops(int id, ProductsFilterParams filter = null)
        {
            var product = _productRepository.Read(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Filter = filter;
            ViewBag.IsIssetInBookmark = _productBookmarkRepository.IsIsset(WebSecurity.CurrentUserId, product.Id);
            return View(product);
        }


        public ActionResult PopularProducts()
        {
            var products = _popularProductRepository.Read();
            return View(products);
        }


        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
