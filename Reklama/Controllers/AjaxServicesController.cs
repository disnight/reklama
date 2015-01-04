using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Announcements;
using Domain.Entity.Catalogs;
using Domain.Entity.Realty;
using Domain.Entity.Catalogs;
using Domain.Entity.Shared;
using Domain.Enums;
using Domain.Repository.Admin;
using Domain.Repository.Announcements;
using Domain.Repository.Articles;
using Domain.Repository.Shared;
using Domain.Repository.Realty;
using Domain.Repository.Catalogs;
using Reklama.Core.UploadImages;
using Reklama.Models.SortModels;
using WebMatrix.WebData;
using Reklama.Models;

namespace Reklama.Controllers
{
    public class AjaxServicesController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();
        private readonly ImageProvider _imageProvider = new ImageProvider();

        private ISubsectionRepository _subsectionRepository;
        private IProfileRepository _profileRepository;
        private IAnnouncementBookmarkRepository _announcementBookmarkRepository;
        private IRealtyBookmarkRepository _realtyBookmarkRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IAnnouncementImageRepository _announcementImageRepository;
        private readonly IConfigRepository _configRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleSectionRepository _articleSectionRepository;
        private readonly IArticleSubsectionRepository _articleSubsectionRepository;
        private readonly IRealtyRepository _realtyRepository;
        private readonly IRealtyPhotoRepository _realtyPhotoRepository;
        private readonly IShopRepository _shopRepository;
        private readonly ICatalogSecondCategoryRepository _catalogSecondCategoryRepository;
        private readonly ICatalogThirdCategoryRepository _catalogThirdCategoryRepository;
        private readonly IShopCategoryRefRepository _shopCategoryRefRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IShopProductRefRepository _shopProductRefRepository;
        private readonly IProductBookmarkRepository _productBookmarkRepository;
        private readonly IProductRepository _productRepository;

        public AjaxServicesController(
            ISubsectionRepository subsectionRepository,
            IProfileRepository profileRepository,
            IAnnouncementBookmarkRepository announcementBookmarkRepository,
            IAnnouncementRepository announcementRepository,
            IAnnouncementImageRepository announcementImageRepository,
            IConfigRepository configRepository,
            IArticleRepository articleRepository,
            IArticleSectionRepository articleSectionRepository,
            IArticleSubsectionRepository articleSubsectionRepository,
            IRealtyRepository realtyRepository,
            IRealtyPhotoRepository realtyPhotoRepository,
            IRealtyBookmarkRepository realtyBookmarkRepository,
            IShopRepository shopRepository,
            ICatalogSecondCategoryRepository catalogSecondCategoryRepository,
            ICatalogThirdCategoryRepository catalogThirdCategoryRepository,
            IShopCategoryRefRepository shopCategoryRefRepository,
            IProductImageRepository productImageRepository,
            IShopProductRefRepository shopProductRefRepository,
            IProductBookmarkRepository productBookmarkRepository,
            IProductRepository productRepository)
        {
            _subsectionRepository = subsectionRepository;
            _subsectionRepository.Context = rc;

            _profileRepository = profileRepository;
            _profileRepository.Context = rc;

            _announcementBookmarkRepository = announcementBookmarkRepository;
            _announcementBookmarkRepository.Context = rc;

            _announcementRepository = announcementRepository;
            _announcementRepository.Context = rc;

            _announcementImageRepository = announcementImageRepository;
            _announcementImageRepository.Context = rc;

            _configRepository = configRepository;
            _configRepository.Context = rc;

            _articleRepository = articleRepository;
            _articleRepository.Context = rc;

            _articleSectionRepository = articleSectionRepository;
            _articleSectionRepository.Context = rc;

            _articleSubsectionRepository = articleSubsectionRepository;
            _articleSubsectionRepository.Context = rc;

            _realtyRepository = realtyRepository;
            _realtyRepository.Context = rc;

            _realtyPhotoRepository = realtyPhotoRepository;
            _realtyPhotoRepository.Context = rc;

            _realtyBookmarkRepository = realtyBookmarkRepository;
            _realtyBookmarkRepository.Context = rc;

            _shopRepository = shopRepository;
            _shopRepository.Context = rc;

            _catalogSecondCategoryRepository = catalogSecondCategoryRepository;
            _catalogSecondCategoryRepository.Context = rc;

            _catalogThirdCategoryRepository = catalogThirdCategoryRepository;
            _catalogThirdCategoryRepository.Context = rc;

            _shopCategoryRefRepository = shopCategoryRefRepository;
            _shopCategoryRefRepository.Context = rc;

            _productImageRepository = productImageRepository;
            _productImageRepository.Context = rc;

            _shopProductRefRepository = shopProductRefRepository;
            _shopProductRefRepository.Context = rc;

            _productBookmarkRepository = productBookmarkRepository;
            _productBookmarkRepository.Context = rc;

            _productRepository = productRepository;
            _productRepository.Context = rc;
        }


        /**
         * Получает подразделы для объявлений
         */
        [HttpPost]
        public JsonResult GetSubsections(int sectionId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var subsections = new List<object>();

            foreach (var s in _subsectionRepository.ReadBySection(sectionId).OrderBy(s => s.Name))
            {
                subsections.Add(new { Id = s.Id, Name = s.Name });
            }

            return Json(subsections);
        }

        /**
         * Получает категории 2го уровня для товаров
         */
        [HttpPost]
        public JsonResult GetSecondCategory(int categoryId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var subsections = new List<object>();

            foreach (var s in _catalogSecondCategoryRepository.ReadByCategory(categoryId).OrderBy(s => s.Name))
            {
                subsections.Add(new { Id = s.Id, Name = s.Name });
            }

            return Json(subsections);
        }

        /**
         * Получает категории 2го уровня для товаров
         */
        [HttpPost]
        public JsonResult GetThirdCategory(int category2Id)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var subsections = new List<object>();

            foreach (var s in _catalogThirdCategoryRepository.ReadBySecondCategory(category2Id).OrderBy(s => s.Name))
            {
                subsections.Add(new { Id = s.Id, Name = s.Name });
            }

            return Json(subsections);
        }


        /**
         * Получает подразделы для статей
         */
        [HttpPost]
        public JsonResult GetArticleSubsections(int sectionId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var subsections = new List<object>();

            foreach (var s in _articleSubsectionRepository.ReadBySection(sectionId))
            {
                subsections.Add(new { Id = s.Id, Name = s.Name });
            }

            return Json(subsections);
        }


        /// <summary>
        /// Асинхронная загрузка пользовательских файлов на сервер
        /// Загружает файлы изображений для объявлений Announcement
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFile()
        {
            var file = Request.Files.Get("myfile");
            var uploader = new ImageUploader(file);

            var result = new
                             {
                                 filename = uploader.Name,
                                 contentType = uploader.ContentType,
                                 contentLength = uploader.ContentLength,
                                 newFilename = uploader.UniqueName
                             };

            try
            {
                uploader.Convert(530, 350);
                uploader.Save("users");
                uploader.Convert(ProjectConfiguration.Get.AnnouncementImageWidth, ProjectConfiguration.Get.AnnouncementImageHeight);
                uploader.Save("announcement_thumb");
            }
            catch (Exception)
            {
                return Json(new { status = "fail" }, "text/html");
            }
            return Json(result, "text/html");
        }

        /// <summary>
        /// Асинхронная загрузка пользовательских файлов на сервер
        /// Загружает файлы изображений для объявлений Announcement
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult angularUploadFile(string cAction, string cController)
        {
            var file = Request.Files.Get("file");
            var uploader = new ImageUploader(file);

            var result = new
            {
                filename = uploader.Name,
                contentType = uploader.ContentType,
                contentLength = uploader.ContentLength,
                newFilename = uploader.UniqueName
            };

            try
            {
                switch (cController)
                {
                    case "Announcement":
                        uploader.Convert(530, 350);
                        uploader.Save("users");
                        uploader.Convert(ProjectConfiguration.Get.AnnouncementImageWidth, ProjectConfiguration.Get.AnnouncementImageHeight);
                        uploader.Save("announcement_thumb");
                        break;
                    case "Realty":
                        uploader.Convert(530, 350);
                        uploader.Save("realty");
                        uploader.Convert(ProjectConfiguration.Get.RealtyImageWidth, ProjectConfiguration.Get.RealtyImageHeight);
                        uploader.Save("realty_thumb");
                        break;
                    case "Product":
                        uploader.Convert(530, 350);
                        uploader.Save("products");
                        uploader.Convert(ProjectConfiguration.Get.RealtyImageWidth, ProjectConfiguration.Get.RealtyImageHeight);
                        uploader.Save("product_thumb");
                        break;
                }

            }
            catch (Exception)
            {
                return Json(new { status = "fail" }, "text/html");
            }
            return Json(result, "text/html");
        }

        /// <summary>
        /// Асинхронная загрузка пользовательских файлов на сервер
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadRealtyImage()
        {
            var file = Request.Files.Get("myfile");
            var uploader = new ImageUploader(file);

            var result = new
            {
                filename = uploader.Name,
                contentType = uploader.ContentType,
                contentLength = uploader.ContentLength,
                newFilename = uploader.UniqueName
            };

            try
            {
                uploader.Save("realty");
                uploader.Convert(ProjectConfiguration.Get.RealtyImageWidth, ProjectConfiguration.Get.RealtyImageHeight);
                uploader.Save("realty_thumb");
            }
            catch (Exception)
            {
                return Json(new { status = "fail" }, "text/html");
            }
            return Json(result, "text/html");
        }

        /// <summary>
        /// Асинхронная загрузка изображений товаров на сервер
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult ProductAddPhoto(int id)
        {
            var file = Request.Files.Get("myfile");
            var uploader = new FileUploader(file);

            var result = new
            {
                filename = uploader.Name,
                contentType = uploader.ContentType,
                contentLength = uploader.ContentLength,
                newFilename = uploader.UniqueName
            };
            try
            {
                uploader.Save("products");
                _productImageRepository.Save(new ProductImage()
                                                 {
                                                     ProductId = id,
                                                     Link = uploader.UniqueName
                                                 });
            }
            catch (Exception)
            {
                return Json(new { status = "fail" }, "text/html");
            }
            return Json(result, "text/html");
        }

        public JsonResult RemoveImage(string image, string cAction, string cController)
        {
            var result = false;
            switch (cController)
            {
                case "Announcement":
                    result = _imageProvider.RemoveImage(image, ImagesType.Anouncement);
                    break;
                case "Realty":
                    result = _imageProvider.RemoveImage(image, ImagesType.Realty);
                    break;
            }
            return Json(!result ? new { status = "fail" } : new { status = "success" }, "text/html");
        }


        /**
         * Удаление изображения для объявления
         * Удаляется только из ФС, но не из БД
         */
        [HttpPost]
        [Authorize]
        public JsonResult RemoveAnnouncementOnlyImage(string image)
        {
            try
            {
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
        [Authorize]
        public JsonResult RemoveRealtyOnlyImage(string image)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + image;
                System.IO.File.Delete(path);
                path = AppDomain.CurrentDomain.BaseDirectory + ProjectConfiguration.Get.FilePaths["realty_thumb"] + "//" + image.Split('/').Last();
                System.IO.File.Delete(path);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        /**
         * Удаление изображения для статьи
         * Удаляется только из ФС, но не из БД
         */
        [HttpPost]
        [Authorize]
        public JsonResult RemoveArticleImage(int articleId, string image)
        {
            var article = _articleRepository.Read(articleId);

            if (article == null)
            {
                return Json(new { status = "fail" }, "text/html");
            }

            try
            {
                var path = string.Concat(ProjectConfiguration.Get.FilePaths["base"], image);
                System.IO.File.Delete(path);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }


        /**
         * Удаление изображения для объявления
         */
        [HttpPost]
        [Authorize]
        public JsonResult RemoveAnnouncementImage(string announcementId, string image)
        {
            int id = Convert.ToInt32(announcementId);

            try
            {
                var announcement = _announcementRepository.Read(id);

                if (announcement == null)
                {
                    throw new Exception();
                }

                if (announcement.UserId != WebSecurity.CurrentUserId && !User.IsInRole("Administrator"))
                {
                    throw new Exception();
                }

                _announcementImageRepository.DeleteImage(id, image);
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
        [Authorize]
        public JsonResult RemoveRealtyImage(string realtyId, string image)
        {
            int id = Convert.ToInt32(realtyId);

            try
            {
                var realty = _realtyRepository.Read(id);

                if (realty == null)
                {
                    throw new Exception();
                }

                if (realty.UserId != WebSecurity.CurrentUserId && !User.IsInRole("Administrator"))
                {
                    throw new Exception();
                }

                _realtyPhotoRepository.DeleteImage(id, image);
                var path = AppDomain.CurrentDomain.BaseDirectory + image;
                System.IO.File.Delete(path);
                path = AppDomain.CurrentDomain.BaseDirectory + ProjectConfiguration.Get.FilePaths["realty_thumb"] + "//" + image.Split('/').Last();
                System.IO.File.Delete(path);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }


        /**
         * Удаление изображения для товара
         */
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult RemoveProductImage(string image, int id)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + image;
                _productImageRepository.Delete(id, image);
                System.IO.File.Delete(path);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize]
        public JsonResult RemoveShopLogo(string shopId, string image)
        {
            int id = Convert.ToInt32(shopId);

            try
            {
                var shop = _shopRepository.Read(id);

                if (shop == null)
                {
                    throw new Exception();
                }

                if (shop.UserId != WebSecurity.CurrentUserId && !User.IsInRole("Administrator"))
                {
                    throw new Exception();
                }

                shop.ImageLogo = null;
                _shopRepository.Save(shop);
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
        [Authorize]
        public JsonResult AnnouncementUp(string announcementId)
        {
            int id = Convert.ToInt32(announcementId);
            int upTimeAnnouncement = int.Parse(_configRepository.ReadByName("UpTimeAnnouncement").Value);
            var announcement = _announcementRepository.Read(id);

            if (announcement == null)
            {
                return Json(new { status = "fail" }, "text/html");
            }


            if ((WebSecurity.CurrentUserId == announcement.UserId)
                && announcement.UpTime <= DateTime.Now.AddHours(-upTimeAnnouncement)
                || User.IsInRole("Administrator")
                || User.IsInRole("Moderator")
            )
            {
                announcement.UpTime = DateTime.Now;
                announcement.ExpiredAt = DateTime.Now.AddDays(int.Parse(_configRepository.ReadByName("ExpiredAtAnnouncement").Value));

                try
                {
                    _announcementRepository.Save(announcement);
                    return Json(new { status = "success", timeToUp = upTimeAnnouncement }, "text/html");
                }
                catch (Exception)
                {
                    return Json(new { status = "fail" }, "text/html");
                }
            }

            return Json(new { status = "fail" }, "text/html");
        }

        [HttpPost]
        [Authorize]
        public JsonResult RealtyUp(string realtyId)
        {
            int id = Convert.ToInt32(realtyId);
            int upTimeRealty = int.Parse(ProjectConfiguration.Get.GetConfigValue("UpTimeRealty").ToString());
            var realty = _realtyRepository.Read(id);

            if (realty == null)
            {
                return Json(new { status = "fail" }, "text/html");
            }


            if ((WebSecurity.CurrentUserId == realty.UserId)
                && realty.UpTime <= DateTime.Now.AddHours(-upTimeRealty)
                || User.IsInRole("Administrator")
                || User.IsInRole("Moderator")
            )
            {
                realty.UpTime = DateTime.Now;
                realty.ExpiredAt = DateTime.Now.AddDays(int.Parse(ProjectConfiguration.Get.GetConfigValue("ExpiredAtRealty").ToString()));

                try
                {
                    _realtyRepository.Save(realty);
                    return Json(new { status = "success", timeToUp = upTimeRealty }, "text/html");
                }
                catch (Exception)
                {
                    return Json(new { status = "fail" }, "text/html");
                }
            }

            return Json(new { status = "fail" }, "text/html");
        }

        /// <summary>
        /// Загрузка пользовательских аваторов на сервер
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public JsonResult UploadAvatar(string id)
        {
            var file = Request.Files.Get("myfile");
            var uploader = new ImageUploader(file);
            uploader.Quality = CompositingQuality.HighQuality;
            uploader.Interpolation = InterpolationMode.HighQualityBilinear;
            int userId = (User.IsInRole("Administrator")) ? Convert.ToInt32(id) : WebSecurity.CurrentUserId;

            try
            {
                uploader.Convert(ProjectConfiguration.Get.UserAvatarWidth, ProjectConfiguration.Get.UserAvatarHeight);
                _profileRepository.UpdateAvatar(userId, uploader.UniqueName);
                uploader.Save("avatars");
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
            return Json(result, "text/html");
        }

        /// <summary>
        /// Загрузка лого магазина на сервер
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public JsonResult UploadShopLogo(string logo, string id)
        {
            var file = Request.Files.Get("myfile");
            var uploader = new ImageUploader(file);
            uploader.Quality = CompositingQuality.HighQuality;
            uploader.Interpolation = InterpolationMode.HighQualityBilinear;
            int shopId = Convert.ToInt32(id);

            var shop = _shopRepository.Read(shopId);

            try
            {
                if (shop == null || (!User.IsInRole("Administrator") && !User.IsInRole("Moderator") && shop.UserId != WebSecurity.CurrentUserId))
                {
                    throw new Exception();
                }
                shop.ImageLogo = uploader.UniqueName;
                _shopRepository.Save(shop);
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

            if (logo != "/Images/System/no_logo.png")
            {
                RemoveRealtyOnlyImage(logo);
            }
            return Json(result, "text/html");
        }

        public ActionResult AddToBookmarks(CategorySearchSortModel model)
        {
            var status = "success";

            try
            {
                switch (model.Category)
                {
                    case CategorySearch.Announcement:
                        _announcementBookmarkRepository.Save(new AnnouncementBookmark()
                                                                 {
                                                                     AnnouncementId = model.Id,
                                                                     UserId = WebSecurity.CurrentUserId
                                                                 });
                        break;

                    case CategorySearch.Realty:
                        _realtyBookmarkRepository.Save(new RealtyBookmark()
                                                            {
                                                                RealtyId = model.Id,
                                                                UserId = WebSecurity.CurrentUserId
                                                            });
                        break;

                    case CategorySearch.Product:
                        _productBookmarkRepository.Save(new ProductBookmark()
                                                            {
                                                                ProductId = model.Id,
                                                                UserId = WebSecurity.CurrentUserId
                                                            });
                        break;

                    default:
                        status = "fail";
                        break;
                }
            }
            catch
            {
                status = "fail";
            }

            return Json(new { status = status }, "text/html");
        }


        public ActionResult RemoveFromBookmarks(CategorySearchSortModel model)
        {
            var status = "success";

            try
            {
                switch (model.Category)
                {
                    case CategorySearch.Announcement:
                        try
                        {
                            var announcementBookmark = _announcementBookmarkRepository.Read(WebSecurity.CurrentUserId, model.Id);
                            _announcementBookmarkRepository.Delete(announcementBookmark);
                        }
                        catch
                        {
                            status = "fail";
                        }
                        break;

                    case CategorySearch.Realty:
                        try
                        {
                            var realtyBookmark = _realtyBookmarkRepository.Read(WebSecurity.CurrentUserId, model.Id);
                            _realtyBookmarkRepository.Delete(realtyBookmark);
                        }
                        catch
                        {
                            status = "fail";
                        }
                        break;

                    case CategorySearch.Product:
                        try
                        {
                            var productBookmark = _productBookmarkRepository.Read(WebSecurity.CurrentUserId, model.Id);
                            _productBookmarkRepository.Delete(productBookmark);
                        }
                        catch
                        {
                            status = "fail";
                        }
                        break;

                    default:
                        status = "fail";
                        break;
                }
            }
            catch
            {
                status = "fail";
            }

            return Json(new { status = status }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult AddCategoryToShop(int categoryId, int shopId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                _shopCategoryRefRepository.Save(new ShopCategoryRef() { CategoryId = categoryId, ShopId = shopId });
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult RemoveCategoryFromShop(int categoryId, int shopId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                _shopCategoryRefRepository.Delete(categoryId, shopId);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetSecondCategories(int shopId, int categoryId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var secondCategories = new List<object>();

            foreach (var s in _shopCategoryRefRepository.ReadByShop(shopId).Where(x => x.Category.CategoryId == categoryId))
            {
                secondCategories.Add(new { Id = s.CategoryId, Name = s.Category.Name });
            }

            return Json(secondCategories);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public JsonResult GetCatalogSecondCategory(int categoryId)
        {
            var secondCategories = new List<object>();

            foreach (var c in _catalogSecondCategoryRepository.ReadByCategory(categoryId))
            {
                secondCategories.Add(new { Id = c.Id, Name = c.Name });
            }

            return Json(secondCategories);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult GetThirdCategories(int secondCategoryId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var thirdCategories = new List<object>();
            var thirdCat = _catalogThirdCategoryRepository.ReadBySecondCategory(secondCategoryId);
            if (thirdCat.Count() != 0)
            {
                thirdCategories.Add(new { Id = 0, Name = "Все" });
            }
            foreach (var s in _catalogThirdCategoryRepository.ReadBySecondCategory(secondCategoryId))
            {
                thirdCategories.Add(new { Id = s.Id, Name = s.Name });
            }

            return Json(thirdCategories);
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
                _shopProductRefRepository.Save(new ShopProductRef() { ProductId = productId, ShopId = shopId, Price = price });
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
                _shopProductRefRepository.Delete(productId, shopId);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult UpdateShopProduct(int id, decimal price)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            var shopProduct = _shopProductRefRepository.Read(id);
            shopProduct.Price = price;

            if (WebSecurity.CurrentUserId != shopProduct.ShopId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                return Json(new { status = "fail" }, "text/html");
            }

            try
            {
                _shopProductRefRepository.Save(shopProduct);
            }
            catch
            {
                return Json(new { status = "fail" }, "text/html");
            }

            return Json(new { status = "success" }, "text/html");
        }


        public ActionResult AnnouncementOverLife()
        {
            var inactivedItems = _announcementRepository.ReadActivesQuery()
                .Where(i => i.ExpiredAt > DateTime.Now);

            foreach (var item in inactivedItems)
            {
                item.IsAuction = false;
                _announcementRepository.Save(item);
            }
            return null;
        }

        public ActionResult RealtyOverLife()
        {
            var inactivedItems = _realtyRepository.ReadActivesQuery()
                .Where(i => i.ExpiredAt > DateTime.Now);

            foreach (var item in inactivedItems)
            {
                item.IsAuction = false;
                _realtyRepository.Save(item);
            }
            return null;
        }


        public ActionResult ResizeAnnouncementImages()
        {
            var images = _announcementRepository.Read().Select(a => a.Images.OrderBy(x => x.CreatedAt).FirstOrDefault().Link);

            foreach (var image in images)
            {
                if (image != null && !image.Equals(""))
                {
                    var file = ProjectConfiguration.Get.FilePaths["base"] + ProjectConfiguration.Get.FilePaths["users"] + image;
                    var filestream = System.IO.File.OpenRead(file);

                    Bitmap _image;
                    int Width;
                    int Height;
                    CompositingQuality Quality;
                    InterpolationMode Interpolation;
                    CompositingMode Mode;

                    int maxWidth = ProjectConfiguration.Get.AnnouncementImageWidth;
                    int maxHeight = ProjectConfiguration.Get.AnnouncementImageHeight;

                    try
                    {
                        _image = new Bitmap(filestream);
                    }
                    catch (ArgumentException)
                    {
                        throw new HttpException(404, "Photo not found.");
                    }

                    Width = _image.Width;
                    Height = _image.Height;
                    Quality = CompositingQuality.HighSpeed;
                    Interpolation = InterpolationMode.Default;
                    Mode = CompositingMode.SourceCopy;

                    if (Width > maxWidth)
                    {
                        float koef = (float)Width / maxWidth;
                        Width = maxWidth;
                        Height = (int)(Height / koef);
                    }

                    if (Height > maxHeight && Height <= 2 * maxHeight)
                    {
                        float koef = (float)Height / maxHeight;
                        Height = maxHeight;
                        Width = (int)(Width / koef);
                    }
                    else if (Height > 2 * maxHeight)
                    {
                        Height = 2 * maxHeight;
                    }

                    var target = new Bitmap(Width, Height);
                    using (Graphics graphics = Graphics.FromImage(target))
                    {
                        graphics.CompositingQuality = Quality;
                        graphics.InterpolationMode = Interpolation;
                        graphics.CompositingMode = Mode;
                        graphics.DrawImage(_image, 0, 0, Width, Height);
                    }

                    _image = target;

                    _image.Save(Path.Combine(ProjectConfiguration.Get.FilePaths["base"] + ProjectConfiguration.Get.FilePaths["announcement_thumb"], image));
                }
            }

            Response.Output.Write("Ваш бы*лоКод успешно выполнен");

            return null;
        }

        public ActionResult ResizeRealtyImages()
        {
            var images = _realtyRepository.Read().Select(a => a.Photos.OrderBy(x => x.CreatedAt).FirstOrDefault().Link);

            foreach (var image in images)
            {
                if (image != null && !image.Equals(""))
                {
                    var file = ProjectConfiguration.Get.FilePaths["base"] + ProjectConfiguration.Get.FilePaths["realty"] + image;
                    var filestream = System.IO.File.OpenRead(file);

                    Bitmap _image;
                    int Width;
                    int Height;
                    CompositingQuality Quality;
                    InterpolationMode Interpolation;
                    CompositingMode Mode;

                    int maxWidth = ProjectConfiguration.Get.RealtyImageWidth;
                    int maxHeight = ProjectConfiguration.Get.RealtyImageHeight;

                    try
                    {
                        _image = new Bitmap(filestream);
                    }
                    catch (ArgumentException)
                    {
                        throw new HttpException(404, "Photo not found.");
                    }

                    Width = _image.Width;
                    Height = _image.Height;
                    Quality = CompositingQuality.HighSpeed;
                    Interpolation = InterpolationMode.Default;
                    Mode = CompositingMode.SourceCopy;

                    if (Width > maxWidth)
                    {
                        float koef = (float)Width / maxWidth;
                        Width = maxWidth;
                        Height = (int)(Height / koef);
                    }

                    if (Height > maxHeight && Height <= 2 * maxHeight)
                    {
                        float koef = (float)Height / maxHeight;
                        Height = maxHeight;
                        Width = (int)(Width / koef);
                    }
                    else if (Height > 2 * maxHeight)
                    {
                        Height = 2 * maxHeight;
                    }

                    var target = new Bitmap(Width, Height);
                    using (Graphics graphics = Graphics.FromImage(target))
                    {
                        graphics.CompositingQuality = Quality;
                        graphics.InterpolationMode = Interpolation;
                        graphics.CompositingMode = Mode;
                        graphics.DrawImage(_image, 0, 0, Width, Height);
                    }

                    _image = target;

                    _image.Save(Path.Combine(ProjectConfiguration.Get.FilePaths["base"] + ProjectConfiguration.Get.FilePaths["realty_thumb"], image));
                }
            }

            Response.Output.Write("Ваш бы*лоКод успешно выполнен");

            return null;
        }

        public ActionResult ResizeProductImages()
        {
            var images = _productRepository.Read().Select(a => a.Image);

            foreach (var image in images)
            {
                try
                {
                    if (image != null && !image.Equals(""))
                    {
                        var file = ProjectConfiguration.Get.FilePaths["base"] +
                                   ProjectConfiguration.Get.FilePaths["products"] + image;
                        var filestream = System.IO.File.OpenRead(file);

                        Bitmap _image;
                        int Width;
                        int Height;
                        CompositingQuality Quality;
                        InterpolationMode Interpolation;
                        CompositingMode Mode;

                        int maxWidth = ProjectConfiguration.Get.ProductThumbImageWidth;
                        int maxHeight = ProjectConfiguration.Get.ProductThumbImageHeight;

                        try
                        {
                            _image = new Bitmap(filestream);
                        }
                        catch (ArgumentException)
                        {
                            throw new HttpException(404, "Photo not found.");
                        }

                        Width = _image.Width;
                        Height = _image.Height;
                        Quality = CompositingQuality.HighSpeed;
                        Interpolation = InterpolationMode.Default;
                        Mode = CompositingMode.SourceCopy;

                        if (Width > maxWidth)
                        {
                            float koef = (float)Width / maxWidth;
                            Width = maxWidth;
                            Height = (int)(Height / koef);
                        }

                        if (Height > maxHeight && Height <= 2 * maxHeight)
                        {
                            float koef = (float)Height / maxHeight;
                            Height = maxHeight;
                            Width = (int)(Width / koef);
                        }
                        else if (Height > 2 * maxHeight)
                        {
                            Height = 2 * maxHeight;
                        }

                        var target = new Bitmap(Width, Height);
                        using (Graphics graphics = Graphics.FromImage(target))
                        {
                            graphics.CompositingQuality = Quality;
                            graphics.InterpolationMode = Interpolation;
                            graphics.CompositingMode = Mode;
                            graphics.DrawImage(_image, 0, 0, Width, Height);
                        }

                        _image = target;

                        _image.Save(
                            Path.Combine(
                                ProjectConfiguration.Get.FilePaths["base"] +
                                ProjectConfiguration.Get.FilePaths["product_thumb"], image));
                    }
                }
                catch { }
            }

            Response.Output.Write("Ваш бы*лоКод успешно выполнен");

            return null;
        }


        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
