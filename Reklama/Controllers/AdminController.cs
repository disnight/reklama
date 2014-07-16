using System.Drawing.Drawing2D;
using Domain.Entity.Admin;
using Domain.Entity.Catalogs;
using Domain.Entity.Other;
using Domain.Entity.Shared;
using Domain.Repository.Admin;
using Domain.Repository.Announcements;
using Domain.Repository.Articles;
using Domain.Repository.Catalogs;
using Domain.Repository.Other;
using Domain.Repository.Shared;
using Domain.ViewModels.Banners;
using Reklama.Data.Entities;
using Reklama.Data.Servises;
using Reklama.Models;
using Reklama.Models.ViewModels.Admin;
using Reklama.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using PagedList;

namespace Reklama.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class AdminController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IConfigRepository _configRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuPageRefRepository _menuPageRefRepository;
        private readonly IPageRepository _pageRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPremiumRepository _premiumRepository;
        private readonly IAnnouncementConfigRepository _announcementConfigRepository;
        private readonly IArticleConfigRepository _articleConfigRepository;
        private readonly IMainPageArticleConfigRepository _mainPageArticleConfigRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IAnnouncementsPremiumsRefRepository _announcementsPremiumsRefRepository;
        private readonly ICatalogParamSubsectionRepository _catalogParamSubsectionRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductParamRepository _productParamRepository;
        private readonly IProductParamValueRepository _productParamValueRepository;
        private readonly IProductSectionParamRefRepository _productSectionParamRefRepository;
        private readonly IProductSubsectionParamRefRepository _productSubsectionParamRefRepository;
        private readonly ICatalogCategoryRepository _catalogCategoryRepository;
        private readonly ICatalogSecondCategoryRepository _catalogSecondCategoryRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IShopCategoryRefRepository _shopCategoryRefRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ICatalogConfigRepository _catalogConfigRepository;
        private readonly ICatalogThirdCategoryRepository _catalogThirdCategoryRepository;
        private readonly IPopularSectionInCatalogRepository _popularSectionInCatalogRepository;
        private readonly INewSectionInCatalogRepository _newSectionInCatalogRepository;
        private readonly IPopularProductRepository _popularProductRepository;
        private readonly IPopularAnnouncementRepository _popularAnnouncementRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubsectionRepository _subsectionRepository;


        public AdminController(IConfigRepository configRepository, IPageRepository pageRepository,
            IMenuRepository menuRepository, IMenuPageRefRepository menuPageRefRepository,
            ICurrencyRepository currencyRepository, IPremiumRepository premiumRepository,
            IAnnouncementConfigRepository announcementConfigRepository, IArticleConfigRepository articleConfigRepository,
            IProfileRepository profileRepository, IAnnouncementRepository announcementRepository,
            IArticleRepository articleRepository, IAnnouncementsPremiumsRefRepository announcementsPremiumsRefRepository,
            IUnitRepository unitRepository, IProductParamRepository productParamRepository,
            IProductSectionParamRefRepository productSectionParamRefRepository, IProductSubsectionParamRefRepository productSubsectionParamRefRepository,
            IProductRepository productRepository, ICatalogCategoryRepository catalogCategoryRepository,
            IShopRepository shopRepository, ICatalogSecondCategoryRepository catalogSecondCategoryRepository, IShopCategoryRefRepository shopCategoryRefRepository,
            IProductImageRepository productImageRepository, IProductParamValueRepository productParamValueRepository,
            ICatalogParamSubsectionRepository catalogParamSubsectionRepository, ICatalogConfigRepository catalogConfigRepository,
            ICatalogThirdCategoryRepository catalogThirdCategoryRepository, IPopularSectionInCatalogRepository popularSectionInCatalogRepository,
            INewSectionInCatalogRepository newSectionInCatalogRepository, IPopularProductRepository popularProductRepository,
            IMainPageArticleConfigRepository mainPageArticleConfigRepository, IPopularAnnouncementRepository popularAnnouncementRepository,
            ISectionRepository sectionRepository, ISubsectionRepository subsectionRepository)
        {
            _configRepository = configRepository;
            _configRepository.Context = rc;

            _pageRepository = pageRepository;
            _pageRepository.Context = rc;

            _menuRepository = menuRepository;
            _menuRepository.Context = rc;

            _menuPageRefRepository = menuPageRefRepository;
            _menuPageRefRepository.Context = rc;

            _currencyRepository = currencyRepository;
            _currencyRepository.Context = rc;

            _premiumRepository = premiumRepository;
            _premiumRepository.Context = rc;

            _announcementConfigRepository = announcementConfigRepository;
            _announcementConfigRepository.Context = rc;

            _articleConfigRepository = articleConfigRepository;
            _articleConfigRepository.Context = rc;

            _profileRepository = profileRepository;
            _profileRepository.Context = rc;

            _announcementRepository = announcementRepository;
            _announcementRepository.Context = rc;

            _articleRepository = articleRepository;
            _articleRepository.Context = rc;

            _announcementsPremiumsRefRepository = announcementsPremiumsRefRepository;
            _announcementsPremiumsRefRepository.Context = rc;

            _catalogParamSubsectionRepository = catalogParamSubsectionRepository;
            _catalogParamSubsectionRepository.Context = rc;

            _unitRepository = unitRepository;
            _unitRepository.Context = rc;

            _productParamRepository = productParamRepository;
            _productParamRepository.Context = rc;

            _productSectionParamRefRepository = productSectionParamRefRepository;
            _productSectionParamRefRepository.Context = rc;

            _productSubsectionParamRefRepository = productSubsectionParamRefRepository;
            _productSubsectionParamRefRepository.Context = rc;

            _productRepository = productRepository;
            _productRepository.Context = rc;

            _catalogCategoryRepository = catalogCategoryRepository;
            _catalogCategoryRepository.Context = rc;

            _shopRepository = shopRepository;
            _shopRepository.Context = rc;

            _catalogSecondCategoryRepository = catalogSecondCategoryRepository;
            _catalogSecondCategoryRepository.Context = rc;

            _shopCategoryRefRepository = shopCategoryRefRepository;
            _shopCategoryRefRepository.Context = rc;

            _productImageRepository = productImageRepository;
            _productImageRepository.Context = rc;

            _productParamValueRepository = productParamValueRepository;
            _productParamValueRepository.Context = rc;

            _catalogConfigRepository = catalogConfigRepository;
            _catalogConfigRepository.Context = rc;

            _catalogThirdCategoryRepository = catalogThirdCategoryRepository;
            _catalogThirdCategoryRepository.Context = rc;

            _popularSectionInCatalogRepository = popularSectionInCatalogRepository;
            _popularSectionInCatalogRepository.Context = rc;

            _newSectionInCatalogRepository = newSectionInCatalogRepository;
            _newSectionInCatalogRepository.Context = rc;

            _popularProductRepository = popularProductRepository;
            _popularProductRepository.Context = rc;

            _mainPageArticleConfigRepository = mainPageArticleConfigRepository;
            _mainPageArticleConfigRepository.Context = rc;

            _popularAnnouncementRepository = popularAnnouncementRepository;
            _popularAnnouncementRepository.Context = rc;

            _sectionRepository = sectionRepository;
            _sectionRepository.Context = rc;

            _subsectionRepository = subsectionRepository;
            _subsectionRepository.Context = rc;
        }

        /**
         * Общие настройки
         */
        #region General
        [Authorize(Roles = "Administrator")]
        public ActionResult General()
        {
            List<Config> configs = _configRepository.Read().ToList();
            return View(configs);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult General(List<Config> configs)
        {
            try
            {
                foreach (var config in configs)
                {
                    _configRepository.Save(config);
                }
                TempData["Message"] = "Все изменения сохранены";
            }
            catch (Exception e)
            {
                TempData["Message"] = "Произошла ошибка! " + e.Message;
            }
            return View(configs);
        }
        #endregion



        /**
         * Настройки страниц
         */
        #region Pages
        [Authorize(Roles = "Administrator")]
        public ActionResult Pages()
        {
            var userAgreement = _configRepository.ReadByName("UserAgreement");
            var pageAgreementId = Convert.ToInt32(userAgreement.Value);
            var upAds = _configRepository.ReadByName("HowToUpAnnouncement");
            var pageUpAdsId = Convert.ToInt32(upAds.Value);
            var pages = _pageRepository.Read();

            var configs = new List<PagesConfigViewModel>()
                              {
                                  new PagesConfigViewModel(userAgreement, pages, pageAgreementId),
                                  new PagesConfigViewModel(upAds, pages, pageUpAdsId)
                              };

            return View(configs);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Pages(int configId, int page, string isEnabled)
        {
            try
            {
                var config = _configRepository.Read(configId);

                config.Value = page.ToString();
                config.IsEnable = (isEnabled != null);

                _configRepository.Save(config);
            }
            catch (DataException)
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            TempData["notice"] = "Конфигурация успешно обновлена!";
            return RedirectToAction("Pages");
        }



        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult PageIndex()
        {
            var pages = _pageRepository.Read();
            return View(pages);
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult PageCreate()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        [ValidateAntiForgeryToken]
        public ActionResult PageCreate(Page page)
        {
            page.PageTemplateId = 1;
            page.CreatedAt = DateTime.Now;

            try
            {
                if (ModelState.IsValid)
                {
                    int id = _pageRepository.Save(page);
                    return RedirectToAction("Details", "Page", new { id = id });
                }
            }
            catch (DataException e)
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return View(page);
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult PageEdit(int id)
        {
            var page = _pageRepository.Read(id);

            if (page == null)
            {
                return HttpNotFound();
            }

            return View(page);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        [ValidateAntiForgeryToken]
        public ActionResult PageEdit(Page page)
        {
            var length = page.Description.Length;
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _pageRepository.Save(page);
                    return RedirectToAction("Details", "Page", new { id = id });
                }
            }
            catch (DataException)
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return View(page);
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult PageDelete(int id)
        {
            var page = _pageRepository.Read(id);

            if (page != null)
            {
                try
                {
                    _pageRepository.Delete(page);
                }
                catch (DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return RedirectToAction("PageIndex");
        }
        #endregion



        /**
         * Работа с меню
         */
        #region Menu

        /**
         * Список меню-страниц
         * Вызывается из MenuManage
         */
        [Authorize(Roles = "Administrator")]
        public ActionResult MenusPages()
        {
            var menusPages = _menuPageRefRepository.Read().OrderBy(m => m.Menu.Name);
            return View(menusPages);
        }


        /**
         * Управление меню
         * Для вывода ссылок в меню под логотипом, выпадающее меню, меню футера и пр.
         */
        [Authorize(Roles = "Administrator")]
        public ActionResult MenuManage()
        {
            PopulateMenuDropDownList();
            PopulatePageDropDownList();

            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult MenuManage(MenuPageRef menuPageRef)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _menuPageRefRepository.Save(menuPageRef);
                }
                catch (DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return RedirectToAction("MenuManage", "Admin");
            }

            PopulateMenuDropDownList();
            PopulatePageDropDownList();

            return View(menuPageRef);
        }


        /**
         * Удаление страницы-меню из меню ;)
         */
        [Authorize(Roles = "Administrator")]
        public ActionResult MenuDelete(int id)
        {
            var reflink = _menuPageRefRepository.Read(id);

            if (reflink == null)
            {
                return HttpNotFound();
            }

            _menuPageRefRepository.Delete(reflink);
            return RedirectToAction("MenuManage", "Admin");
        }
        #endregion




        /**
         * Управление валютами
         */
        #region Currency
        [Authorize(Roles = "Administrator")]
        public ActionResult Currency()
        {
            var currencies = _currencyRepository.Read();
            return View(currencies);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CurrencyCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CurrencyCreate(Currency currency)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _currencyRepository.Save(currency);
                    return RedirectToAction("Currency");
                }
                catch
                {
                    TempData["error"] = "Возникла ошибка при создании валюты";
                }
            }

            return View(currency);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CurrencyEdit(int id)
        {
            var currency = _currencyRepository.Read(id);

            if (currency == null)
            {
                return HttpNotFound();
            }

            return View(currency);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CurrencyEdit(Currency currency)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _currencyRepository.Save(currency);
                    return RedirectToAction("Currency");
                }
                catch
                {
                    TempData["error"] = "Возникла ошибка при редактировании валюты";
                }
            }

            return View(currency);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CurrencyDelete(int id)
        {
            var currency = _currencyRepository.Read(id);

            if (currency == null)
            {
                return HttpNotFound();
            }

            try
            {
                _currencyRepository.Delete(currency);
            }
            catch
            {
                TempData["error"] = "Возникла ошибка при удалении валюты";
            }

            return RedirectToAction("Currency");
        }
        #endregion




        /**
         * Управление примиумами
         */
        #region Premium
        [Authorize(Roles = "Administrator")]
        public ActionResult Premium()
        {
            var premiums = _premiumRepository.Read();
            return View(premiums);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult PremiumCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult PremiumCreate(Premium premium)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _premiumRepository.Save(premium);
                    return RedirectToAction("Premium");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(premium);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult PremiumEdit(int id)
        {
            var premium = _premiumRepository.Read(id);

            if (premium == null)
            {
                return HttpNotFound();
            }

            return View(premium);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult PremiumEdit(Premium premium)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _premiumRepository.Save(premium);
                    return RedirectToAction("Premium");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(premium);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult PremiumDelete(int id)
        {
            var premium = _premiumRepository.Read(id);

            if (premium == null)
            {
                return HttpNotFound();
            }

            try
            {
                _premiumRepository.Delete(premium);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("Premium");
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult AnnouncementPremiumUp(int announcementId, int premiumId, int sectionId)
        {
            var premium = _premiumRepository.Read(premiumId);

            if (premium == null)
            {
                return HttpNotFound();
            }

            var announcementPremiumRef = new AnnouncementsPremiumsRef()
            {
                AdId = announcementId,
                AdSectionId = sectionId,
                PremiumId = premiumId,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddHours(premium.Lifetime)
            };


            try
            {
                _announcementsPremiumsRefRepository.Save(announcementPremiumRef);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }


            return RedirectToAction("List", "Announcement", new { SectionId = sectionId });
        }
        #endregion




        /**
         * Конфигурация доски объявлений
         */
        #region AnnouncementConfig
        [Authorize(Roles = "Administrator")]
        public ActionResult AnnouncementConfig()
        {
            var config = _announcementConfigRepository.ReadConfig();
            ViewBag.Premiums = _premiumRepository.Read();
            return View(config);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AnnouncementConfig(AnnouncementConfig config)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _announcementConfigRepository.Save(config);
                    TempData["notice"] = "Конфигурация успешно изменена";
                    return RedirectToAction("AnnouncementConfig");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(config);
        }
        #endregion


        /**
         * Неактивные объявления доски
         */
        #region AnnouncementInactive
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult AnnouncementInactive()
        {
            var announcements =
                _announcementRepository.Read().Where(a => a.IsActive == false).OrderByDescending(a => a.Id);
            return View(announcements);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult AnnouncementMakeActive(int id)
        {
            var ads = _announcementRepository.Read(id);

            if (ads == null)
            {
                return HttpNotFound();
            }

            ads.IsActive = true;

            try
            {
                _announcementRepository.Save(ads);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("AnnouncementInactive");
        }
        #endregion


        /**
         * Конфигурация для статей
         */
        #region ArticleConfig
        [Authorize(Roles = "Administrator")]
        public ActionResult ArticleConfig()
        {
            var config = _articleConfigRepository.ReadConfig();
            return View(config);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult ArticleConfig(ArticleConfig config)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _articleConfigRepository.Save(config);
                    TempData["notice"] = "Конфигурация успешно изменена";
                    return RedirectToAction("ArticleConfig");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(config);
        }
        #endregion


        /**
         * Конфигурация каталога
         */
        #region CatalogConfig

        [Authorize(Roles = "Administrator")]
        public ActionResult CatalogConfig()
        {
            var config = _catalogConfigRepository.ReadConfig();
            return View(config);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CatalogConfig(CatalogConfig config)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _catalogConfigRepository.Save(config);
                    TempData["notice"] = "Конфигурация успешно изменена";
                    return RedirectToAction("CatalogConfig");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(config);
        }

        #endregion


        /**
         * Неактивные статьи
         */
        #region ArticleInactive
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ArticleInactive()
        {
            var articles = _articleRepository.ReadInactive().OrderByDescending(a => a.Id);
            return View(articles);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ArticleMakeActive(int id)
        {
            var article = _articleRepository.Read(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            article.IsActive = true;

            try
            {
                _articleRepository.Save(article);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ArticleInactive");
        }
        #endregion


        /**
         * Настройки пользователей
         */
        #region Users
        [Authorize(Roles = "Administrator")]
        public ActionResult Users()
        {
            var users = _profileRepository.Read().OrderBy(u => u.Email);
            return View(users);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UsersEdit(int id)
        {
            var profile = _profileRepository.Read(id);
            return View(profile);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult UsersEdit(UserProfile profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _profileRepository.Save(profile);
                    return RedirectToAction("Users");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(profile);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UsersChangePassword(int id)
        {
            return View(id);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult UsersChangePassword(int id, string password)
        {
            var user = _profileRepository.Read(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                string token = WebSecurity.GeneratePasswordResetToken(user.Email);

                try
                {
                    if (WebSecurity.ResetPassword(token, password))
                    {
                        TempData["notice"] = string.Format(
                            "Пароль для пользователя '{0}' был успешно изменен на '{1}'",
                            user.Email, password
                        );

                        return RedirectToAction("Users");
                    }
                }
                catch (InvalidOperationException ioe)
                {
                    TempData["error"] = ioe.Message;
                }
            }
            return View(id);
        }


        [Authorize(Roles = "Administrator")]
        public RedirectToRouteResult UsersConfirmAccount(string email)
        {
            if (false)
            {
                TempData["notice"] = "Пользователь активирован";
            }
            else
            {
                TempData["error"] = "Возникла ошибка при активации пользователя";
            }

            return RedirectToAction("Users");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UsersCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult UsersCreate(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = WebSecurity.CreateUserAndAccount(user.Email, user.Password);
                    WebSecurity.ConfirmAccount(user.Email, token);
                    TempData["notice"] = string.Format("Пользователь '{0}' был успешно создан и активирован!", user.Email);
                    return RedirectToAction("Users");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(user);
        }

        [ChildActionOnly]
        public ActionResult RenderUsersRoles(int id)
        {
            var user = _profileRepository.Read(id);
            var roles = Roles.GetRolesForUser(user.Email);
            return View(roles);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UsersRoles(int id)
        {
            var user = _profileRepository.Read(id);
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult UsersAddRole(string role, string email, int id)
        {
            try
            {
                Roles.AddUserToRole(email, role);
                TempData["notice"] = string.Format("Пользователю '{0}' успешно добавлена роль '{1}'", email, role);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }

            return RedirectToAction("UsersRoles", new { id = id });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult UsersDropRole(string role, string email, int id)
        {
            try
            {
                Roles.RemoveUserFromRole(email, role);
                TempData["notice"] = string.Format("У пользователю '{0}' успешно удалена роль '{1}'", email, role);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }

            return RedirectToAction("UsersRoles", new { id = id });
        }

        #endregion



        /**
         * Создание SelectList'ов для выпадающих списков
         */
        #region Populate
        private void PopulateMenuDropDownList(object selectedMenu = null)
        {
            var menus = _menuRepository.Read();
            ViewBag.Menus = new SelectList(menus, "Id", "Name", selectedMenu);
        }

        private void PopulatePageDropDownList(object selectedPage = null)
        {
            var pages = _pageRepository.Read();
            ViewBag.Pages = new SelectList(pages, "Id", "Name", selectedPage);
        }
        #endregion


        /**
         * Товары, и мало что с ними связанное 
         */
        #region Products

        #region ProductParamSection
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSection()
        {
            var categories = _catalogCategoryRepository.Read();
            return View(categories);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSectionCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSectionCreate(CatalogCategory section)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _catalogCategoryRepository.Save(section);
                    TempData["notice"] = "Сохранено";
                    return RedirectToAction("ProductParamSection");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(section);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSectionEdit(int id)
        {
            var section = _catalogCategoryRepository.Read(id);
            return View(section);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSectionEdit(CatalogCategory section)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _catalogCategoryRepository.Save(section);
                    TempData["notice"] = "Сохранено";
                    return RedirectToAction("ProductParamSection");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(section);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSectionDelete(int id)
        {
            try
            {
                var section = _catalogCategoryRepository.Read(id);
                _catalogCategoryRepository.Delete(section);
                TempData["notice"] = "Удалено";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductParamSection");
        }
        #endregion

        #region ProductParamSecondCategory

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSecondCategory(int categoryId)
        {
            var category = _catalogCategoryRepository.Read(categoryId);
            ViewBag.Category = category;
            return View(category.SecondCategories);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSecondCategoryCreate(int categoryId)
        {
            ViewBag.Category = _catalogCategoryRepository.Read(categoryId);
            var secondCategory = new CatalogSecondCategory() {CategoryId = categoryId};
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSecondCategoryCreate(CatalogSecondCategory secondCategory)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _catalogSecondCategoryRepository.Save(secondCategory);
                    TempData["notice"] = "Категория 2 уровня успешно создана";
                    return RedirectToAction("ProductParamSecondCategory", new { categoryId = secondCategory.CategoryId });
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(secondCategory);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSecondCategoryEdit(int id)
        {
            var secondCategory = _catalogSecondCategoryRepository.Read(id);
            PopulateCategory(secondCategory.Category);
            return View(secondCategory);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSecondCategoryEdit(CatalogSecondCategory secondCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _catalogSecondCategoryRepository.Save(secondCategory);
                    TempData["notice"] = "Категория 2 уровня успешно обновлена";
                    return RedirectToAction("ProductParamSecondCategory", new { categoryId = secondCategory.CategoryId });
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            PopulateCategory(secondCategory.Category);
            return View(secondCategory);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSecondCategoryDelete(int id)
        {
            var secondCategory = _catalogSecondCategoryRepository.Read(id);

            try
            {
                _catalogSecondCategoryRepository.Delete(secondCategory);
                TempData["notice"] = "Категория 2 уровня была успешно удалена";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductParamSecondCategory", new { categoryId = secondCategory.CategoryId });
        }

        #endregion

        #region ProductParamThirdCategory

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamThirdCategory(int secondCategoryId)
        {
            var secondCategory = _catalogSecondCategoryRepository.Read(secondCategoryId);
            ViewBag.SecondCategory = secondCategory;
            return View(secondCategory.CatalogThirdCategories);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamThirdCategoryCreate(int secondCategoryId)
        {
            var secondCategory = _catalogSecondCategoryRepository.Read(secondCategoryId);
            ViewBag.SecondCategory = secondCategory;
            var thirdCategory = new CatalogThirdCategory() {SecondCategoryId = secondCategoryId};
            return View(thirdCategory);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamThirdCategoryCreate(CatalogThirdCategory thirdCategory)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _catalogThirdCategoryRepository.Save(thirdCategory);
                    TempData["notice"] = "Производитель успешно создан";
                    return RedirectToAction("ProductParamThirdCategory",
                                            new {secondCategoryId = thirdCategory.SecondCategoryId});
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(thirdCategory);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamThirdCategoryEdit(int id)
        {
            var thirdCategory = _catalogThirdCategoryRepository.Read(id);
            PopulateSecondCategory(thirdCategory.SecondCategory);
            return View(thirdCategory);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamThirdCategoryEdit(CatalogThirdCategory thirdCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _catalogThirdCategoryRepository.Save(thirdCategory);
                    TempData["notice"] = "Производитель успешно обновлен";
                    return RedirectToAction("ProductParamThirdCategory",
                                            new { secondCategoryId = thirdCategory.SecondCategoryId });
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            PopulateSecondCategory(thirdCategory.SecondCategory);
            return View(thirdCategory);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamThirdCategoryDelete(int id)
        {
            var thirdCategory = _catalogThirdCategoryRepository.Read(id);

            try
            {
                _catalogThirdCategoryRepository.Delete(thirdCategory);
                TempData["notice"] = "Производитель успешно удален";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductParamThirdCategory",
                                            new { secondCategoryId = thirdCategory.SecondCategoryId });
        }

        [Authorize(Roles = "Administrator, Moderator")]
        private void PopulateSecondCategory(object selected = null)
        {
            var secondCategories = _catalogSecondCategoryRepository.Read();
            ViewBag.SecondCategories = new SelectList(secondCategories, "Id", "Name", selected);
        }

        #endregion


        #region ProductParamSubsection

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSubsection()
        {
            var subsections = _catalogParamSubsectionRepository.Read().OrderBy(s => s.SecondCategory.Name);
            return View(subsections);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSubsectionCreate()
        {
            PopulateProductParamSecondCategory();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSubsectionCreate(CatalogParamSubsection subsection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _catalogParamSubsectionRepository.Save(subsection);
                    TempData["notice"] = "Сохранено";
                    return RedirectToAction("ProductParamSubsection");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            PopulateProductParamSecondCategory(subsection.SecondCategory);
            return View(subsection);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSubsectionEdit(int id)
        {
            var subsection = _catalogParamSubsectionRepository.Read(id);
            PopulateProductParamSecondCategory(subsection.SecondCategory);
            return View(subsection);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSubsectionEdit(CatalogParamSubsection subsection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _catalogParamSubsectionRepository.Save(subsection);
                    TempData["notice"] = "Сохранено";
                    return RedirectToAction("ProductParamSubsection");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            PopulateProductParamSecondCategory(subsection.SecondCategory);
            return View(subsection);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamSubsectionDelete(int id)
        {
            var subsection = _catalogParamSubsectionRepository.Read(id);

            try
            {
                _catalogParamSubsectionRepository.Delete(subsection);
                TempData["notice"] = "Удалено";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductParamSubsection");
        }

        private void PopulateProductParamSecondCategory(object selected = null)
        {
            var categories = _catalogSecondCategoryRepository.Read();
            ViewBag.ProductParamCategories = new SelectList(categories, "Id", "Name", selected);
        }

        #endregion

        #region Unit

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Unit()
        {
            var units = _unitRepository.Read();
            return View(units);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult UnitCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult UnitCreate(Unit unit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitRepository.Save(unit);
                    TempData["notice"] = "Сохранено";
                    return RedirectToAction("Unit");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(unit);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult UnitEdit(int id)
        {
            var unit = _unitRepository.Read(id);
            return View(unit);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult UnitEdit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitRepository.Save(unit);
                    TempData["notice"] = "Сохранено";
                    return RedirectToAction("Unit");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(unit);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult UnitDelete(int id)
        {
            var unit = _unitRepository.Read(id);

            try
            {
                _unitRepository.Delete(unit);
                TempData["notice"] = "Удалено";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("Unit");
        }

        #endregion

        #region ProductParams

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParams()
        {
            var parames = _productParamRepository.Read();
            return View(parames);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamsCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamsCreate(ProductParam param)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productParamRepository.Save(param);
                    TempData["notice"] = "Сохранено";
                    return RedirectToAction("ProductParams");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(param);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamsEdit(int id)
        {
            var param = _productParamRepository.Read(id);
            return View(param);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamsEdit(ProductParam param)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productParamRepository.Save(param);
                    TempData["notice"] = "Сохранено";
                    return RedirectToAction("ProductParams");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(param);
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamsDelete(int id)
        {
            var param = _productParamRepository.Read(id);

            try
            {
                _productParamRepository.Delete(param);
                TempData["notice"] = "Удалено";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductParams");
        }

        #endregion

        #region ProductSectionParamRef

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSectionParamRefSelectSelection()
        {
            var sections = _catalogCategoryRepository.Read();
            return View(sections);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSectionParamsRef(int categoryId)
        {
            ViewBag.Category = _catalogCategoryRepository.Read(categoryId);
            var refs = _productSectionParamRefRepository.ReadBySection(categoryId);
            return View(refs);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSectionParamsRefCreate(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            var setsParams = _productSectionParamRefRepository.ReadParamsBySection(categoryId).ToArray();
            var parames = _productParamRepository.Read().ToArray();
            var resultParams = parames.Except(setsParams);
            return View(resultParams);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSectionParamsRefCreate(int paramId, int categoryId)
        {
            var paramRef = new ProductSectionParamRef()
                               {
                                   ParamId = paramId,
                                   CategoryId = categoryId
                               };

            try
            {
                _productSectionParamRefRepository.Save(paramRef);
                TempData["notice"] = "Параметр успешно добавлен";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductSectionParamsRef", new { categoryId = categoryId });
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSectionParamsRefDelete(int id)
        {
            var paramRef = _productSectionParamRefRepository.Read(id);

            try
            {
                _productSectionParamRefRepository.Delete(paramRef);
                TempData["notice"] = "Удалено";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductSectionParamsRef", new { categoryId = paramRef.CategoryId });
        }

        #endregion

        #region ProductSubsectionParamRef

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSubsectionParamRefSelectSelection()
        {
            var subsections = _catalogParamSubsectionRepository.Read();
            return View(subsections);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSubsectionParamsRef(int subsectionId)
        {
            ViewBag.Subsection = _catalogParamSubsectionRepository.Read(subsectionId);
            var refs = _productSubsectionParamRefRepository.ReadBySubsection(subsectionId);
            return View(refs);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSubsectionParamsRefCreate(int subsectionId)
        {
            ViewBag.SubsectionId = subsectionId;
            var setsParams = _productSubsectionParamRefRepository.ReadParamsBySubsection(subsectionId).ToArray();
            var parames = _productParamRepository.Read().ToArray();
            var resultParams = parames.Except(setsParams);
            return View(resultParams);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSubsectionParamsRefCreate(int paramId, int subsectionId)
        {
            var paramRef = new ProductSubsectionParamRef()
            {
                ParamId = paramId,
                SubsectionId = subsectionId
            };

            try
            {
                _productSubsectionParamRefRepository.Save(paramRef);
                TempData["notice"] = "Параметр успешно добавлен";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductSubsectionParamsRef", new { subsectionId = subsectionId });
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductSubsectionParamsRefDelete(int id)
        {
            var paramRef = _productSubsectionParamRefRepository.Read(id);

            try
            {
                _productSubsectionParamRefRepository.Delete(paramRef);
                TempData["notice"] = "Удалено";
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductSubsectionParamsRef", new { subsectionId = paramRef.SubsectionId });
        }

        #endregion

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductCreate()
        {
            PopulateCategory();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductCreate(Product product)
        {
            var file = Request.Files["Image"];
            int id = -1;

            if (file == null || file.ContentLength == 0)
            {
                ModelState.AddModelError("Logo", "Вы не загрузили изображение");
            }

            if(!product.SecondCategoryId.HasValue)
            {
                ModelState.AddModelError("SecondCategoryId", "Обязательно укажите");
            }

            var uploader = new ImageUploader(file);
            uploader.Interpolation = InterpolationMode.HighQualityBilinear;
            uploader.Quality = CompositingQuality.HighQuality;
            product.Image = uploader.UniqueName;
            product.CreatedAt = DateTime.Now;


            if (ModelState.IsValid)
            {
                try
                {
                    uploader.Convert(ProjectConfiguration.Get.ProductImageWidth, ProjectConfiguration.Get.ProductImageHeight);
                    uploader.Save("products");
                    uploader.Convert(ProjectConfiguration.Get.AnnouncementImageWidth, ProjectConfiguration.Get.AnnouncementImageHeight);
                    uploader.Save("product_thumb");
                }
                catch (Exception)
                {
                    TempData["error"] = "Возникли проблемы при загрузке изображения. Попробуйте снова";
                    PopulateCategory();
                    return View(product);
                }

                try
                {
                    id = _productRepository.Save(product);
                }
                catch (Exception e)
                {
                    TempData["error"] = "Возникли проблемы при создании товара. Попробуйте снова";
                    PopulateCategory();
                    return View(product);
                }

                return RedirectToAction("Details", "Product", new { id = id });
            }

            PopulateCategory();
            return View(product);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductEdit(int id)
        {
            var product = _productRepository.Read(id);
            PopulateCategory(product.Category);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductEdit(int id, Product product)
        {
            var file = Request.Files["Image"];
            ImageUploader uploader = null;
            var productOld = _productRepository.Read(id);

            if (file == null || file.ContentLength == 0)
            {
                product.Image = productOld.Image;
            }
            else
            {
                uploader = new ImageUploader(file);
                uploader.Interpolation = InterpolationMode.HighQualityBilinear;
                uploader.Quality = CompositingQuality.HighQuality;
                product.Image = uploader.UniqueName;

                try
                {
                    //_productImageRepository.Delete(product.Id, productOld.Image);
                    //ImageUploader.Delete("products", productOld.Image);   /// DOESN'T WORK! :)
                }
                catch
                {
                    TempData["error"] = "Возникла ошибка при удалении прежней фотографии";
                    PopulateCategory(product.Category);
                    return View(product);
                }
            }

            if (ModelState.IsValid)
            {
                if (uploader != null)
                {
                    try
                    {
                        uploader.Convert(ProjectConfiguration.Get.ProductImageWidth, ProjectConfiguration.Get.ProductImageHeight);
                        uploader.Save("products");
                    }
                    catch (Exception)
                    {
                        TempData["error"] = "Возникли проблемы при загрузке изображения. Попробуйте снова";
                        PopulateCategory(product.Category);
                        return View(product);
                    }
                }

                try
                {
                    _productRepository.Save(product);
                }
                catch
                {
                    TempData["error"] = "Возникли проблемы при создании товара. Попробуйте снова";
                    PopulateCategory(product.Category);
                    return View(product);
                }

                return RedirectToAction("Details", "Product", new { id = id });
            }

            PopulateCategory(product.Category);
            return View(product);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductAddPhoto(int id)
        {
            var product = _productRepository.Read(id);
            return View(product);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamValuesEdit(int id)
        {
            var product = _productRepository.Read(id);
            ViewBag.ProductParamValues = _productParamValueRepository.ReadByProduct(id).ToArray().ToList();

            return View(product);
        }


        private void PopulateCategory(object selected = null)
        {
            var categories = _catalogCategoryRepository.Read();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selected);
        }


        #region ProductParamValue

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamValueCreate(int productId, int paramId, int? paramSubsectionId)
        {
            var productParamValue = new ProductParamValue()
            {
                ProductId = productId,
                ParamId = paramId,
                ParamSubsectionId = paramSubsectionId
            };

            PopulateUnit();
            return View(productParamValue);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamValueCreate(ProductParamValue paramValue)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productParamValueRepository.Save(paramValue);
                    return RedirectToAction("ProductParamValuesEdit", new { id = paramValue.ProductId });
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            PopulateUnit(paramValue.Unit);
            return View(paramValue);
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamValueEdit(int id)
        {
            var value = _productParamValueRepository.Read(id);
            PopulateUnit(value.Unit);

            return View(value);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamValueEdit(ProductParamValue paramValue)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productParamValueRepository.Save(paramValue);
                    return RedirectToAction("ProductParamValuesEdit", new { id = paramValue.ProductId });
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            PopulateUnit(paramValue.Unit);
            return View(paramValue);
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult ProductParamValueDelete(int id)
        {
            var value = _productParamValueRepository.Read(id);

            try
            {
                _productParamValueRepository.Delete(value);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("ProductParamValuesEdit", new { id = value.ProductId });
        }


        private void PopulateUnit(object selected = null)
        {
            var units = _unitRepository.Read();
            ViewBag.Units = new SelectList(units, "Id", "Name", selected);
        }

        #endregion

        #endregion

        /**
         * Управление магазинами
         */
        #region Shop
        [Authorize(Roles = "Administrator")]
        public ActionResult ListShop()
        {
            var shops = _shopRepository.Read();
            return View(shops);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ConfirmActivationShop(int id)
        {
            var shop = _shopRepository.Read(id);
            if (shop == null)
            {
                HttpNotFound();
            }
            return View(shop);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ActivationShop(int id)
        {
            var shop = _shopRepository.Read(id);
            if (shop == null)
            {
                HttpNotFound();
            }
            try
            {
                shop.IsActive = true;
                Roles.AddUserToRole(shop.User.Email, "Shop");
                _shopRepository.Save(shop);
            }
            catch
            {
                TempData["error"] = "Возникла ошибка при сохранение магазина, повторите попытку.";
                RedirectToAction("ListShop", "Admin");
            }

            var message =
            string.Format("Поздравляем ваш магазин " + shop.Name +
            " на сайте reklama.tm активирован. Панель правления доступна по адресу <a href=\"reklama.tm/Shop/RegistrationData/"
            + shop.Id + "\">reklama.tm/Shop/RegistrationData/" + shop.Id + "</a>"
            );

            var mailRobot = new MailRobot();
            mailRobot.SetHeaderParams(true, ProjectConfiguration.Get.Email, System.Net.Mail.MailPriority.Normal, true);
            try
            {
                mailRobot.SendMessage(shop.User.Email, "Активация магазина на сайте reklama.tm", message);
            }
            catch (Exception)
            {
                TempData["error"] = "Магазин активирован, но возникла ошибка при отправке письма пользователю.";
                return RedirectToAction("ListShop", "Admin");
            }

            TempData["notice"] = "Магазин успешно активирован, на Email пользователя выслано сообщение.";

            

            return RedirectToAction("ListShop", "Admin");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeactivationShop(int id)
        {
            var shop = _shopRepository.Read(id);
            if (shop == null)
            {
                HttpNotFound();
            }
            try
            {
                shop.IsActive = false;
                Roles.RemoveUserFromRole(shop.User.Email, "Shop");
                _shopRepository.Save(shop);
            }
            catch
            {
                TempData["error"] = "Возникла ошибка при сохранение магазина, повторите попытку.";
                RedirectToAction("ListShop", "Admin");
            }

            var message =
            string.Format("Сожаелеем ваш магазин " + shop.Name +
            " на сайте reklama.tm заблокирован."
            );

            var mailRobot = new MailRobot();
            try
            {
                mailRobot.SendMessage(shop.User.Email, "Блокировка магазина на сайте reklama.tm", message);
            }
            catch (Exception)
            {
                TempData["error"] = "Магазин заблокирован, но возникла ошибка при отправке письма пользователю.";
                return RedirectToAction("ListShop", "Admin");
            }

            TempData["notice"] = "Магазин успешно заблокирован, на Email пользователя выслано сообщение.";

            return RedirectToAction("ListShop", "Admin");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ConfirmDeleteShop(int id)
        {
            var shop = _shopRepository.Read(id);

            if (shop == null)
            {
                HttpNotFound();
            }

            return View(shop);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteShop(int id)
        {
            var shop = _shopRepository.Read(id);

            if (shop == null)
            {
                HttpNotFound();
            }

            try
            {
                _shopRepository.Delete(shop);
            }
            catch
            {
                TempData["error"] = "Произошла ошибка при удалении магазина.";
                return RedirectToAction("ListShop", "Admin");
            }
            TempData["notice"] = "Магазин удален";
            return RedirectToAction("ListShop", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddCategoryToShop(int id)
        {
            var shop = _shopRepository.Read(id);
            if (shop == null)
            {
                HttpNotFound();
            }
            ViewBag.Shop = shop;

            var categories = _catalogSecondCategoryRepository.Read().OrderBy(x => x.CategoryId);
            ViewBag.ShopCategories = _shopCategoryRefRepository.ReadByShop(id);

            return View(categories);
        }
        #endregion

        /**
         * Создание Banner'ов
         */
        #region Banners

        [Authorize(Roles = "Administrator")]
        public ActionResult Banners()
        {
            var service = new BannerService();
            var banners = service.GetAll();
            return View(banners);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult BannersCreate()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult BannersEdit(int id)
        {
            var service = new BannerService();
            var banner = service.GetBanner(id);
            return View(new EditBannerViewModel
            {
                Comments = banner.Comments,
                ID = banner.ID,
                ImagePath = banner.Images != null ? banner.Images.ImagePath : "",
                IsShow = banner.IsShow,
                Link = banner.Link,
                ImageDesc = banner.BannerTypes != null ? banner.BannerTypes.Desc : ""
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult BannersEdit(EditBannerViewModel model)
        {
            var service = new BannerService();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var current = service.GetBanner(model.ID);

            if (model.File != null)
            {
                var uploader = new ImageUploader(model.File);
                uploader.Save("banners");
               
                var path = "/Images/Banners/" + uploader.UniqueName;
                if (current.Images != null)
                {
                    current.Images.ImagePath = path;
                }
                else
                {
                    current.Images = new Images
                    {
                        ImagePath = path
                    };
                }
                model.ImagePath = path;
            }

            current.Comments = model.Comments;
            current.IsShow = model.IsShow;
            current.Link = model.Link;
            service.SaveBanner(current);

            return View(model);
        }
        #endregion


        #region MainPage

        #region MainPagePopularSectionsInCatalog
        public ActionResult MainPagePopularSectionsInCatalog()
        {
            var populars = _popularSectionInCatalogRepository.Read();
            ViewBag.Categories = _catalogCategoryRepository.Read();
            return View(populars);
        }

        public ActionResult MainPagePopularSectionsInCatalogCreate(int subcategoryId)
        {
            var pop = new PopularSectionInCatalog()
                          {
                              SectionId = subcategoryId
                          };
            
            _popularSectionInCatalogRepository.Save(pop);
            return RedirectToAction("MainPagePopularSectionsInCatalog");
        }

        public ActionResult MainPagePopularSectionsInCatalogDelete(int id)
        {
            var p = _popularSectionInCatalogRepository.Read(id);

            _popularSectionInCatalogRepository.Delete(p);
            return RedirectToAction("MainPagePopularSectionsInCatalog");
        }
        #endregion

        #region MainPageNewInCatalog
        public ActionResult MainPageNewInCatalog()
        {
            var ne = _newSectionInCatalogRepository.Read();
            ViewBag.Categories = _catalogCategoryRepository.Read();
            return View(ne);
        }

        public ActionResult MainPageNewInCatalogCreate(int subcategoryId)
        {
            var ne = new NewSectionInCatalog()
                         {
                             SectionId = subcategoryId
                         };

            _newSectionInCatalogRepository.Save(ne);
            return RedirectToAction("MainPageNewInCatalog");
        }

        public ActionResult MainPageNewInCatalogDelete(int id)
        {
            var n = _newSectionInCatalogRepository.Read(id);

            _newSectionInCatalogRepository.Delete(n);
            return RedirectToAction("MainPageNewInCatalog");
        }
        #endregion

        #region MainPagePopularProducts
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult MainPagePopularProducts()
        {
            var products = _popularProductRepository.Read();
            ViewBag.Categories = _catalogCategoryRepository.Read();
            return View(products);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult MainPagePopularProductsCreate(int subcategoryId)
        {
            var products = _productRepository.ReadBySecondCategory(subcategoryId);
            return View(products);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult MainPagePopularProductsCreate2(int id)
        {
            var p = new PopularProduct()
                        {
                            ProductId = id
                        };

            _popularProductRepository.Save(p);
            return RedirectToAction("MainPagePopularProducts");
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult MainPagePopularProductsDelete(int id)
        {
            var p = _popularProductRepository.Read(id);

            _popularProductRepository.Delete(p);
            return RedirectToAction("MainPagePopularProducts");
        }

        #endregion

        #region MainPagePopularAnnouncements
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult MainPagePopularAnnouncements()
        {
            var announcements = _popularAnnouncementRepository.Read();
            ViewBag.Sections = _sectionRepository.Read();
            return View(announcements);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult MainPagePopularAnnouncementCreate(int subsectionId)
        {
            var announcement = _announcementRepository.Read().Where(x => x.SubsectionId == subsectionId);
            return View(announcement);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult MainPagePopularAnnouncementCreate2(int id)
        {
            var announcement = new PopularAnnouncement()
            {
                AnnouncementId = id
            };

            _popularAnnouncementRepository.Save(announcement);
            return RedirectToAction("MainPagePopularAnnouncements");
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult MainPagePopularAnnouncementsDelete(int id)
        {
            var announcement = _popularAnnouncementRepository.Read(id);

            _popularAnnouncementRepository.Delete(announcement);
            return RedirectToAction("MainPagePopularAnnouncements");
        }

        #endregion

        #region MainPageArticlesReviews

        public ActionResult MainPageArticlesReviews()
        {
            var config = _mainPageArticleConfigRepository.ReadConfig();
            return View(config);
        }

        [HttpPost]
        public ActionResult MainPageArticlesReviews(MainPageArticleConfig config)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _mainPageArticleConfigRepository.Save(config);
                    TempData["notice"] = "Конфигурация успешно изменена";
                    return RedirectToAction("MainPageArticlesReviews");
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }
            }

            return View(config);
        }

        #endregion

        #endregion


        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            ProjectConfiguration.Get.Dispose();
            base.Dispose(disposing);
        }
    }
}
