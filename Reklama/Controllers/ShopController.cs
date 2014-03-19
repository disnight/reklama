using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Domain.Repository.Shared;
using Domain.Repository.Admin;
using Reklama.Models;
using PagedList;
using Reklama.Models.ViewModels.Catalog;
using WebMatrix.WebData;

namespace Reklama.Controllers
{
    public class ShopController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly ICityRepository _cityRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IConfigRepository _configRepository;
        private readonly IShopCategoryRefRepository _shopCategoryRefRepository;
        private readonly ICatalogCategoryRepository _catalogCategoryRepository;
        private readonly ICatalogSecondCategoryRepository _catalogSecondCategoryRepository;
        private readonly ICatalogThirdCategoryRepository _catalogThirdCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShopProductRefRepository _shopProductRefRepository;

        public ShopController(ICityRepository cityRepository, IShopRepository shopRepository, IConfigRepository configRepository, 
            IShopCategoryRefRepository shopCategoryRefRepository, ICatalogCategoryRepository catalogCategoryRepository,
            ICatalogSecondCategoryRepository catalogSecondCategoryRepository, ICatalogThirdCategoryRepository catalogThirdCategoryRepository,
            IProductRepository productRepository, IShopProductRefRepository shopProductRefRepository)
        {
            _cityRepository = cityRepository;
            _cityRepository.Context = rc;

            _shopRepository = shopRepository;
            _shopRepository.Context = rc;

            _configRepository = configRepository;
            _configRepository.Context = rc;

            _shopCategoryRefRepository = shopCategoryRefRepository;
            _shopCategoryRefRepository.Context = rc;

            _catalogCategoryRepository = catalogCategoryRepository;
            _catalogCategoryRepository.Context = rc;

            _catalogSecondCategoryRepository = catalogSecondCategoryRepository;
            _catalogSecondCategoryRepository.Context = rc;

            _catalogThirdCategoryRepository = catalogThirdCategoryRepository;
            _catalogThirdCategoryRepository.Context = rc;

            _productRepository = productRepository;
            _productRepository.Context = rc;

            _shopProductRefRepository = shopProductRefRepository;
            _shopProductRefRepository.Context = rc;

            ViewBag.Slogan = ProjectConfiguration.Get.CatalogConfig.Slogan;
        }

        //
        // GET: /Shop/

        public ActionResult Index(int id)
        {
            var shop = _shopRepository.Read(id);
            return View(shop);
        }

        //
        // GET: /Shop/Details/5

        public ActionResult Details(int id, int? commentPage)
        {
            var shop = _shopRepository.Read(id);
            if (shop == null)
            {
                HttpNotFound();
            }
            ViewBag.ShopId = shop.Id;
            ViewBag.Comments = (shop.Comments != null && shop.Comments.Count > 0)
                ? shop.Comments.ToPagedList(commentPage ?? 1, ProjectConfiguration.Get.CommentsOnPage)
                : null;

            return View(shop);
        }

        public ActionResult ViewCategories(int shopId)
        {
            var shop = _shopRepository.Read(shopId);
            if (shop == null)
            {
                HttpNotFound();
            }
            var shopCategoriesRef = _shopCategoryRefRepository.ReadByShop(shopId);
            List<ShopCategoryRef> secondCategories = new List<ShopCategoryRef>();
            foreach (var category in shopCategoriesRef)
            {
                if (_productRepository.ReadBySecondCategory(category.CategoryId).Count() != 0)
                {
                    secondCategories.Add(category);
                }
            }
            List<CatalogCategory> categories = new List<CatalogCategory>();
            var categoryId = secondCategories.GroupBy(x => x.Category.CategoryId);
            foreach (var c in categoryId)
            {
                categories.Add(_catalogCategoryRepository.Read(c.Key));
            }
            ViewBag.Categories = categories;
            //ViewBag.SecondCategories = secondCategories;
            return PartialView("_Categories", secondCategories);
        }

        //
        // GET: /Shop/Create
        [Authorize]
        public ActionResult Create()
        {
            if (_shopRepository.IsExistShopByCurrentUser(WebSecurity.CurrentUserId))
            {
                TempData["error"] = "У вас уже создан магазин!";
                return Redirect("/");
            }
            ViewBag.Cities = _cityRepository.Read();
            return View();
        }

        //
        // POST: /Shop/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Shop shop)
        {
            if (ModelState.IsValid)
            {
                shop.UserId = WebSecurity.CurrentUserId;
                try
                {
                    _shopRepository.Save(shop);
                    TempData["notice"] = "Магазин создан, личная панель будет доступна после активации магазина администратором.";
                    return Redirect("/");
                }
                catch
                {
                    TempData["Error"] = "Произошла ошибка при создании магазина";
                    ViewBag.Cities = _cityRepository.Read();
                    return View(shop);
                }
            }
            return View(shop);
        }

        //
        //GET: /Shop/RegistrationData/5 

        [Authorize]
        public ActionResult RegistrationData(int id)
        {
            var shop = _shopRepository.Read(id);

            if (shop == null || (WebSecurity.CurrentUserId != shop.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserId && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            ViewBag.Cities = _cityRepository.Read();
            ViewBag.ChangeRegistrationDataHelp = _configRepository.ReadByName("ChangeRegistrationDataHelp");
            if(_shopCategoryRefRepository.ReadByShop(id).Count() != 0)
                ViewBag.MonthlyFee = _shopCategoryRefRepository.ReadByShop(id).Sum(x => x.Category.Price);
            return View(shop);
        }


        [Authorize]
        public ActionResult RegistrationDataEdit(int id)
        {
            var shop = _shopRepository.Read(id);

            if (shop == null || (WebSecurity.CurrentUserId != shop.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserId && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            ViewBag.Cities = _cityRepository.Read();
            ViewBag.ChangeRegistrationDataHelp = _configRepository.ReadByName("ChangeRegistrationDataHelp");
            if (_shopCategoryRefRepository.ReadByShop(id).Count() != 0)
                ViewBag.MonthlyFee = _shopCategoryRefRepository.ReadByShop(id).Sum(x => x.Category.Price);

            return View(shop);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RegistrationDataEdit(Shop model)
        {
            ViewBag.Cities = _cityRepository.Read();
            
            var shop = _shopRepository.Read(model.Id);

            if (WebSecurity.CurrentUserId == shop.UserId || User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        shop.Phone = model.Phone;
                        shop.CityId = model.CityId;
                        shop.Requisites = model.Requisites;
                        if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
                        {
                            shop.Name = model.Name;
                            shop.FullName = model.FullName;
                            shop.CompanyName = model.CompanyName;
                        }
                        _shopRepository.Save(shop);
                        return RedirectToAction("RegistrationData", "Shop", new { id = shop.Id });
                    }
                    catch
                    {
                        TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                        return View(model);
                    }
                }
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                return View(model);
            }
            else
            {
                return RedirectToRoute("RestrictedAccess");
            }
        }

        public ActionResult ViewShopPage(int id)
        {
            var shop = _shopRepository.Read(id);
            if (_shopCategoryRefRepository.ReadByShop(id).Count() != 0)
                ViewBag.MonthlyFee = _shopCategoryRefRepository.ReadByShop(id).Sum(x => x.Category.Price);

            if (shop == null || (WebSecurity.CurrentUserId != shop.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserId && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            return View(shop);
        }

        [Authorize]
        public ActionResult EditShopPage(int id)
        {
            var shop = _shopRepository.Read(id);
            if (_shopCategoryRefRepository.ReadByShop(id).Count() != 0)
                ViewBag.MonthlyFee = _shopCategoryRefRepository.ReadByShop(id).Sum(x => x.Category.Price);

            if (shop == null || (WebSecurity.CurrentUserId != shop.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserId && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            return View(shop);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditShopPage(Shop model)
        {
            var shop = _shopRepository.Read(model.Id);

            if (WebSecurity.CurrentUserId == shop.UserId || User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                if (ModelState.IsValid)
                {
                    
                    try
                    {
                        shop.Phone = model.Phone;
                        shop.Site = model.Site;
                        shop.Icq = model.Icq;
                        shop.Skype = model.Skype;
                        shop.Description = model.Description;
                        shop.Monday = model.Monday;
                        shop.Tuesday = model.Tuesday;
                        shop.Wednesday = model.Wednesday;
                        shop.Thursday = model.Tuesday;
                        shop.Friday = model.Friday;
                        shop.Saturday = model.Saturday;
                        shop.Sunday = model.Sunday;
                        if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
                        {
                            shop.Name = model.Name;
                        }
                        _shopRepository.Save(shop);
                        return RedirectToAction("ViewShopPage", "Shop", new { id = shop.Id });
                    }
                    catch
                    {
                        TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                        return View(model);
                    }
                }
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                return View(model);
            }
            else
            {
                return RedirectToRoute("RestrictedAccess");
            }
        }

        [HttpGet]
        public ActionResult BaseProducts(FilterParams filterParams = null)
        {
            var shop = _shopRepository.Read(filterParams.Id);

            //Verification access
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserId && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var shopCategories = _shopCategoryRefRepository.ReadByShop(filterParams.Id);

            if (shopCategories.Count() == 0)
            {
                TempData["error"] = "У магазина нет категорий!";
                return Redirect("/");
            }

            var shopProducts = _shopCategoryRefRepository.ReadByShop(filterParams.Id);

            //Read the first level category
            List<CatalogCategory> categories = new List<CatalogCategory>();
            var categoryId = shopCategories.GroupBy(x => x.Category.CategoryId);
            foreach (var c in categoryId)
            {
                categories.Add(_catalogCategoryRepository.Read(c.Key));
            }

            //Read the second level category
            var selectCategory = filterParams.CategoryId == 0 ? categories[0] : categories.FirstOrDefault(x => x.Id == filterParams.CategoryId);
            var secondCategories = shopCategories.Where(x => x.Category.CategoryId == selectCategory.Id).ToList();

            //Read the third level category
            var selectSecondCategory = filterParams.SecondCategoryId == 0 ? secondCategories[0] : secondCategories.FirstOrDefault(x => x.CategoryId == filterParams.SecondCategoryId);
            var thirdCategories = _catalogThirdCategoryRepository.ReadBySecondCategory(selectSecondCategory.CategoryId).ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectCategory);
            ViewBag.SecondCategories = new SelectList(secondCategories, "CategoryId", "Category.Name", selectSecondCategory);
            ViewBag.Shop = shop;
            ViewBag.FilterParams = filterParams;
            ViewBag.MonthlyFee = shopCategories.Sum(x => x.Category.Price);

            //Organization list of products
            PagedList<Product> products;

            if (thirdCategories.Count() != 0)
            {
                thirdCategories.Add(new CatalogThirdCategory() { Id = 0, Name = "Все" });
                thirdCategories = thirdCategories.OrderBy(x => x.Id).ToList();
                var selectThirdCategory = thirdCategories.FirstOrDefault(x => x.Id == filterParams.ThirdCategoryId);//filterParams.ThirdCategoryId == 0 ? thirdCategories[0] : thirdCategories.FirstOrDefault(x => x.Id == filterParams.ThirdCategoryId);
                ViewBag.ThirdCategories = new SelectList(thirdCategories, "Id", "Name", selectThirdCategory);
                if (filterParams.ThirdCategoryId != 0)
                {
                    products = new PagedList<Product>(_productRepository.ReadByThirdCategory(selectThirdCategory.Id).OrderBy(x => x.CreatedAt), filterParams.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);
                    ViewBag.ShopProducts = _shopProductRefRepository.ReadProductsByThirdCategory(filterParams.Id, selectThirdCategory.Id);
                }
                else
                {
                    products = new PagedList<Product>(_productRepository.ReadBySecondCategory(selectSecondCategory.CategoryId).OrderBy(x => x.CreatedAt), filterParams.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);
                    ViewBag.ShopProducts = _shopProductRefRepository.ReadProductsBySecondCategory(filterParams.Id, selectSecondCategory.CategoryId);
                }
            }
            else
            {
                ViewBag.ThirdCategories = null;
                products = new PagedList<Product>(_productRepository.ReadBySecondCategory(selectSecondCategory.CategoryId).OrderBy(x => x.CreatedAt), filterParams.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);
                ViewBag.ShopProducts = _shopProductRefRepository.ReadProductsBySecondCategory(filterParams.Id, selectSecondCategory.CategoryId);
            }

            return View(products);
        }

        [Authorize(Roles = "Shop")]
        public ActionResult RedirectToBaseProducts()
        {
            var userId = WebSecurity.CurrentUserId;
            var id = _shopRepository.ReadShopIdByUserId(userId);
            return RedirectToAction("BaseProducts", new {id = id});
        }

        public ActionResult ProductsShop(FilterParams filterParams)
        {
            var shop = _shopRepository.Read(filterParams.Id);

            //Verification access
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserId && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var shopCategories = _shopCategoryRefRepository.ReadByShop(filterParams.Id);

            if (shopCategories.Count() == 0)
            {
                TempData["error"] = "У магазина нет категорий!";
                return Redirect("/");
            }

            //Read the first level category
            List<CatalogCategory> categories = new List<CatalogCategory>();
            var categoryId = shopCategories.GroupBy(x => x.Category.CategoryId);
            foreach (var c in categoryId)
            {
                categories.Add(_catalogCategoryRepository.Read(c.Key));
            }

            //Read the second level category
            var selectCategory = filterParams.CategoryId == 0 ? categories[0] : categories.FirstOrDefault(x => x.Id == filterParams.CategoryId);
            var secondCategories = shopCategories.Where(x => x.Category.CategoryId == selectCategory.Id).ToList();

            //Read the third level category
            var selectSecondCategory = filterParams.SecondCategoryId == 0 ? secondCategories[0] : secondCategories.FirstOrDefault(x => x.CategoryId == filterParams.SecondCategoryId);
            var thirdCategories = _catalogThirdCategoryRepository.ReadBySecondCategory(selectSecondCategory.CategoryId).ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectCategory);
            ViewBag.SecondCategories = new SelectList(secondCategories, "CategoryId", "Category.Name", selectSecondCategory);
            ViewBag.Shop = shop;
            ViewBag.FilterParams = filterParams;
            ViewBag.MonthlyFee = shopCategories.Sum(x => x.Category.Price);

            //Organization list of products
            PagedList<ShopProductRef> products;

            if (thirdCategories.Count() != 0)
            {
                thirdCategories.Add(new CatalogThirdCategory() { Id = 0, Name = "Все" });
                thirdCategories = thirdCategories.OrderBy(x => x.Id).ToList();
                var selectThirdCategory = thirdCategories.FirstOrDefault(x => x.Id == filterParams.ThirdCategoryId);//filterParams.ThirdCategoryId == 0 ? thirdCategories[0] : thirdCategories.FirstOrDefault(x => x.Id == filterParams.ThirdCategoryId);
                ViewBag.ThirdCategories = new SelectList(thirdCategories, "Id", "Name", selectThirdCategory);
                if (filterParams.ThirdCategoryId != 0)
                {
                    products = new PagedList<ShopProductRef>(_shopProductRefRepository.ReadProductsByThirdCategory(filterParams.Id, selectThirdCategory.Id).OrderBy(x => x.Id), filterParams.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);
                }
                else
                {
                    products = new PagedList<ShopProductRef>(_shopProductRefRepository.ReadProductsBySecondCategory(filterParams.Id, selectSecondCategory.CategoryId).OrderBy(x => x.Id), filterParams.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);
                }
            }
            else
            {
                ViewBag.ThirdCategories = null;
                products = new PagedList<ShopProductRef>(_shopProductRefRepository.ReadProductsBySecondCategory(filterParams.Id, selectSecondCategory.CategoryId).OrderBy(x => x.Id), filterParams.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);
            }

            return View(products);
        }

        public ActionResult RemoveProductFromShop(FilterParams filterParams, int shopProductId)
        {
            var shopProduct = _shopProductRefRepository.Read(shopProductId);

            //Verification access
            if (shopProduct == null || (WebSecurity.CurrentUserId != shopProduct.Shop.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }

            try
            {
                _shopProductRefRepository.Delete(shopProduct);
            }
            catch
            {
                TempData["error"] = "Произошла ошибка при удалении товара";
            }
            return RedirectToAction("ProductsShop", filterParams);
        }

        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
