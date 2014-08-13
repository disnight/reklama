using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Catalogs;
using Domain.Repository.Admin;
using Domain.Repository.Shared;
using PagedList;
using Reklama.Data.Entities;
using Reklama.Data.Servises;
using Reklama.Filters;
using Reklama.Models;
using Reklama.Models.ViewModels.Catalog;
using Reklama.ViewModels.Catalog;
using Reklama.ViewModels.Shops;
using WebMatrix.WebData;

namespace Reklama.Controllers
{
    public class ShopsController : Controller
    {
        private readonly ShopsService _shopService = new ShopsService();
        private readonly ProductService _productService = new ProductService();
        private readonly IProfileRepository _profileRepository;
        private readonly IConfigRepository _configRepository;

        public ShopsController(IProfileRepository profileRepository, IConfigRepository configRepository)
        {

            _profileRepository = profileRepository;
            _configRepository = configRepository;
            var rc = new ReklamaContext();
            _configRepository.Context = rc;
        }

        public ActionResult Details(int id, int? commentPage)
        {
            var shop = _shopService.GetShop(id);
            if (shop == null)
            {
                HttpNotFound();
            }
            _profileRepository.Context = new ReklamaContext();
            var result = new ShopDetailsPageViewModel(_profileRepository) {Shop = shop};

            if (shop.ShopFeedback.Any())
            {
                result.Comments = shop.ShopFeedback.Select(q => new FeedbackViewModel(_profileRepository)
                {
                    Comment = q.Comment,
                    ID = q.ID,
                    UserID = q.UserID,
                    CreatedAt = q.CreatedAt
                }).ToPagedList(commentPage ?? 1,
                    ProjectConfiguration.Get.CommentsOnPage);
            }

            return View(result);
        }

        public ActionResult CategoriesView(int shopID)
        {
            return PartialView("_Categories", new CategoriesViewModel
            {
                Groups = _shopService.GetShopCategories(shopID),
                ShopID = shopID
            });
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult FeedbackDelete(int commentId)
        {
            var comment = _shopService.GetShopFeedback(commentId);

            if (comment == null)
            {
                return HttpNotFound();
            }
            try
            {
                _shopService.ShopFeedbackDelete(commentId);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }
            return RedirectToAction("Details", "Shops", new { id = comment.ShopID });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult CreateShopFeedback(ShopCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _shopService.ShopFeedbackCreate(new Data.Entities.ShopFeedback
                    {
                        Comment = commentViewModel.Comment,
                        ShopID = commentViewModel.ShopId,
                        CreatedAt = DateTime.Now,
                        UserID = WebSecurity.CurrentUserId
                    });
                }
                catch (DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return RedirectToAction("Details", "Shops", new { id = commentViewModel.ShopId });
            }
            else
            {
                TempData["Comment"] = commentViewModel.Comment;
                return RedirectToAction("Details", "Shops", new { id = commentViewModel.ShopId });
            }
        }

        public ActionResult ProductsShop(int shopID, int categoryID, ShopProductsFilter filter = null)
        {
            if (filter == null)
            {
                filter = new ShopProductsFilter {CategoryID = categoryID, ShopID = shopID};
            }
            var result = new ShopProductsViewModel();

            var shopProducts = _shopService.GetShopProducts(filter.ShopID, filter.CategoryID).Sort(filter.SortOrder, filter.SortOptions, false);

            result.Category = _productService.GetCategory(categoryID);
            result.Products = shopProducts.Select(q => q.Product).ToPagedList(filter.Page, filter.PageSize);
            result.Filter = filter;

            return View(result);
        }
        public ActionResult ViewShopPage(int id)
        {
            var shop = _shopService.GetShop(id);
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            var result = new ShopPageViewModel() {Shop = shop};

            var categories = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories.Any())
                result.MonthlyFee = categories.Sum(x => x.Price);

            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            return View(result);
        }

        [Authorize]
        public ActionResult EditShopPage(int id)
        {
            var shop = _shopService.GetShop(id);
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }

            _profileRepository.Context = new ReklamaContext();
            var result = new ShopPageViewModel() { Shop = shop };

            var categories = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories.Any())
                result.MonthlyFee = categories.Sum(x => x.Price);

            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            return View(result);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditShopPage(ShopPageViewModel model)
        {
            var shop = _shopService.GetShop(model.Shop.ID);

            if (WebSecurity.CurrentUserId == shop.UserID || User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        shop.Phone = model.Shop.Phone;
                        shop.Site = model.Shop.Site;
                        shop.Icq = model.Shop.Icq;
                        shop.Skype = model.Shop.Skype;
                        shop.Description = model.Shop.Description;
                        shop.Monday = model.Shop.Monday;
                        shop.Tuesday = model.Shop.Tuesday;
                        shop.Wednesday = model.Shop.Wednesday;
                        shop.Thursday = model.Shop.Tuesday;
                        shop.Friday = model.Shop.Friday;
                        shop.Saturday = model.Shop.Saturday;
                        shop.Sunday = model.Shop.Sunday;
                        if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
                        {
                            shop.Title = model.Shop.Title;
                        }
                        _shopService.Save();
                        return RedirectToAction("ViewShopPage", "Shops", new { id = shop.ID });
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

        

        [Authorize]
        public ActionResult RegistrationData(int id)
        {
            var shop = _shopService.GetShop(id);

            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var result = new ShopRegistrationDataViewModel();

            var categories = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories.Any())
                result.MonthlyFee = categories.Sum(x => x.Price);
            result.Shop = shop;
            result.ChangeRegistrationDataHelp = _configRepository.ReadByName("ChangeRegistrationDataHelp").Value;

            return View(result);
        }


        [Authorize]
        public ActionResult RegistrationDataEdit(int id)
        {
            var shop = _shopService.GetShop(id);

            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var result = new ShopRegistrationDataViewModel();

            var categories = shop.ShopProduct.Select(q => q.Product.Category).Distinct().ToList();
            if (categories.Any())
                result.MonthlyFee = categories.Sum(x => x.Price);
            result.Shop = shop;
            result.ChangeRegistrationDataHelp = _configRepository.ReadByName("ChangeRegistrationDataHelp").Value;

            return View(result);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RegistrationDataEdit(ShopRegistrationDataViewModel model)
        {
            var shop = _shopService.GetShop(model.Shop.ID);

            if (WebSecurity.CurrentUserId == shop.UserID || User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        shop.Phone = model.Shop.Phone;
                        shop.CityID = model.Shop.CityID;
                        shop.Requisites = model.Shop.Requisites;
                        if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
                        {
                            shop.Title = model.Shop.Title;
                            shop.CompanyName = model.Shop.CompanyName;
                        }
                        _shopService.Save();
                        return RedirectToAction("RegistrationData", "Shops", new { id = shop.ID });
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
        public ActionResult BaseProducts(ProductForShopViewModel model, FilterParams filter = null)
        {
            if(filter == null) 
                filter = new FilterParams();

            var shop = _shopService.GetShop(filter.Id);

            //Verification access
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var groups = _shopService.GetGroups().ToList();
            var selectedGroup = filter.CategoryId == 0
                ? groups.First()
                : groups.First(q => q.ID == filter.CategoryId);

            var categories = _shopService.GetCategories(selectedGroup.ID);
            var selectedCategory = filter.SecondCategoryId == 0
                ? categories.First()
                : categories.First(q => q.ID == filter.SecondCategoryId);

            var emptyManufacturer = new Manufacturer {ID = 0, Name = "Все"};
            var manufacturers = new List<Manufacturer> { emptyManufacturer };
            manufacturers.AddRange(_shopService.GetManufacturers(selectedCategory.ID));
            Manufacturer selectedManufacturer = emptyManufacturer;
            if (manufacturers.Count > 1)
            {
                selectedManufacturer = filter.ThirdCategoryId == 0
                ? manufacturers.First()
                : manufacturers.First(q => q.ID == filter.ThirdCategoryId);
            }

            var shopProducts = _shopService.GetShopProducts(shop.ID);

            model.Filter = filter;
            model.Groups = new SelectList(groups, "ID", "Name", selectedGroup);
            model.Categories = new SelectList(categories, "ID", "Name", selectedCategory);
            model.Manufacturers = new SelectList(manufacturers, "ID", "Name", selectedManufacturer);
            model.Shop = shop;
            model.MonthlyFee = 0;
            model.ShopProducts = shopProducts;
            model.Products = selectedCategory.Product.Where(q => selectedManufacturer.ID == 0 || selectedManufacturer.ID == q.ManufacturerID).ToPagedList(filter.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);
                
            

            return View(model);
        }

        public ActionResult ShopProducts(ProductForShopViewModel model, FilterParams filter = null)
        {
            if (filter == null)
                filter = new FilterParams();

            var shop = _shopService.GetShop(filter.Id);

            //Verification access
            if (shop == null || (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator")))
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == shop.UserID && shop.IsActive == false && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                TempData["error"] = "Магазин еще не активирован!";
                return Redirect("/");
            }

            var source = _shopService.GetShopCategories(shop.ID).ToList();
            var groups = new List<Group>();

            if (source.Any()) { 
                groups.AddRange(source.Select(q => q.Key));
                var selectedGroup = filter.CategoryId == 0
                    ? groups.First()
                    : groups.First(q => q.ID == filter.CategoryId);

                var categories = new List<Category>();
                categories.AddRange(source.FirstOrDefault(q => q.Key.ID == selectedGroup.ID).Distinct());
                var selectedCategory = filter.SecondCategoryId == 0
                    ? categories.First()
                    : categories.First(q => q.ID == filter.SecondCategoryId);

                var emptyManufacturer = new Manufacturer { ID = 0, Name = "Все" };
                var manufacturers = new List<Manufacturer> { emptyManufacturer };
                manufacturers.AddRange(_shopService.GetCategoryManufacturers(shop.ID, selectedCategory.ID));
                Manufacturer selectedManufacturer = emptyManufacturer;
                if (manufacturers.Count > 1)
                {
                    selectedManufacturer = filter.ThirdCategoryId == 0
                    ? manufacturers.First()
                    : manufacturers.First(q => q.ID == filter.ThirdCategoryId);
                }

                var shopProducts = _shopService.GetShopProducts(shop.ID).ToList();

                model.Filter = filter;
                model.Groups = new SelectList(groups, "ID", "Name", selectedGroup);
                model.Categories = new SelectList(categories, "ID", "Name", selectedCategory);
                model.Manufacturers = new SelectList(manufacturers, "ID", "Name", selectedManufacturer);
                model.ShopProducts = shopProducts;
                model.ShopProductsFiltered = shopProducts.Where(q => q.Product.CategoryID == selectedCategory.ID).Where(q => selectedManufacturer.ID == 0 || selectedManufacturer.ID == q.Product.ManufacturerID).ToPagedList(filter.Page, ProjectConfiguration.Get.ProductsOnPageInBasePage);

                }

                model.Shop = shop;
                model.MonthlyFee = 0;

            return View(model);
        }

        [Authorize(Roles = "Shop")]
        public ActionResult RedirectToBaseProducts()
        {
            var userId = WebSecurity.CurrentUserId;
            var shop = _shopService.GetShopByUserID(userId);
            return RedirectToAction("BaseProducts", new { id = shop.ID });
        }

        #region AJAX Methods

        [HttpPost]
        [Authorize]
        public JsonResult UploadShopLogo(string logo, string id)
        {
            var file = Request.Files.Get("myfile");
            var uploader = new ImageUploader(file);
            uploader.Quality = CompositingQuality.HighQuality;
            uploader.Interpolation = InterpolationMode.HighQualityBilinear;
            var shopId = Convert.ToInt32(id);

            var shop = _shopService.GetShop(shopId);

            try
            {
                if (shop == null || (!User.IsInRole("Administrator") && !User.IsInRole("Moderator") && shop.UserID != WebSecurity.CurrentUserId))
                {
                    throw new Exception();
                }
                shop.Logo = uploader.UniqueName;
                _shopService.Save();
                uploader.Convert(ProjectConfiguration.Get.ShopLogoWidth, ProjectConfiguration.Get.ShopLogoHeight);
                uploader.Save("shopLogo");
            }
            catch (Exception)
            {
                return Json(new { status = "fail" }, "text/html");
            }

            var result = new
            {
                newFilename = uploader.UniqueName,
                status = "success"
            };

            //if (logo != "/Images/System/no_logo.png")
            //{
            //    RemoveRealtyOnlyImage(logo);
            //}
            return Json(result, "text/html");
        }

        [HttpPost]
        [Authorize]
        public JsonResult RemoveShopLogo(string shopId, string image)
        {
            var id = Convert.ToInt32(shopId);

            try
            {
                var shop = _shopService.GetShop(id);

                if (shop == null)
                {
                    throw new Exception();
                }

                if (shop.UserID != WebSecurity.CurrentUserId && !User.IsInRole("Administrator"))
                {
                    throw new Exception();
                }

                shop.Logo = null;
                _shopService.Save();
                var path = AppDomain.CurrentDomain.BaseDirectory + image;
                System.IO.File.Delete(path);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetCategories(int shopId, int groupId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var secondCategories = new List<object>();

            foreach (var s in _shopService.GetCategories(groupId))
            {
                secondCategories.Add(new { Id = s.ID, s.Name });
            }

            return Json(secondCategories);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetShopCategories(int shopId, int groupId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var categories = _shopService.GetShopCategories(shopId, groupId).FirstOrDefault();
            if (categories == null)
            {
                throw new InvalidCastException("Not an correct group");
            }

            var secondCategories = new List<object>();

            foreach (var s in categories)
            {
                secondCategories.Add(new { Id = s.ID, s.Name });
            }

            return Json(secondCategories);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetManufacturers(int categoryId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var result = new List<object>();
            var manufacturers = _shopService.GetManufacturers(categoryId).ToList();
            if (manufacturers.Any())
            {
                result.Add(new { Id = 0, Name = "Все" });
            }
            foreach (var s in manufacturers)
            {
                result.Add(new { Id = s.ID, s.Name });
            }

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetShopManufacturers(int shopId, int categoryId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var result = new List<object>();
            var manufacturers = _shopService.GetCategoryManufacturers(shopId, categoryId).ToList();
            if (manufacturers.Any())
            {
                result.Add(new { Id = 0, Name = "Все" });
            }
            foreach (var s in manufacturers)
            {
                result.Add(new { Id = s.ID, s.Name });
            }

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult AddProductToShop(int productId, int shopId, decimal price)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                _shopService.AddProductToShop(new ShopProduct() { ProductID = productId, ShopID = shopId, Price = price, ShopProductStatusID = 1});
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult UpdateShopProduct(int shopId, int productId, decimal price)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var shop = _shopService.GetShop(shopId);
            if (shop == null)
            {
                return Json(new { status = "fail" }, "text/html");
            }


            if (WebSecurity.CurrentUserId != shop.UserID && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                return Json(new { status = "fail" }, "text/html");
            }

            try
            {
                _shopService.ShopProductsUpdate(shopId, productId, price);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult RemoveProductFromShop(int productId, int shopId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                _shopService.RemoveProductFromShop(productId, shopId);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        #endregion
    }
}
