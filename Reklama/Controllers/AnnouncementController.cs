using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Announcements;
using Domain.Entity.Common;
using Domain.Repository.Admin;
using Domain.Repository.Announcements;
using Domain.Repository.Other;
using Domain.Repository.Shared;
using Reklama.Attributes;
using Reklama.Core.UploadImages;
using Reklama.Filters;
using Reklama.Models;
using Reklama.Models.SortModels;
using Reklama.Models.ViewModels.Announcement;
using Reklama.Services;
using WebMatrix.WebData;
using PagedList;
using Domain.Enums;

namespace Reklama.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly ReklamaContext rc = new ReklamaContext();

        private readonly IAnnouncementRepository _repository;
        private readonly IAnnouncementBookmarkRepository _bookmarkRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAnnouncementImageRepository _announcementImageRepository;
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IConfigRepository _configRepository;
        private readonly IAnnouncementsPremiumsRefRepository _announcementsPremiumsRefRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IComputerRepository _computerRepository;
        private readonly IComputerAnnouncementRefRepository _computerAnnouncementRefRepository;
        private readonly AnonymousUserService _anonymousUserService = new AnonymousUserService();

        public AnnouncementController(IAnnouncementRepository repository, 
            ISectionRepository sectionRepository,
            ICategoryRepository categoryRepository,
            IAnnouncementImageRepository announcementImageRepository,
            ISubsectionRepository subsectionRepository,
            ICityRepository cityRepository,
            ICurrencyRepository currencyRepository,
            IConfigRepository configRepository,
            IAnnouncementBookmarkRepository bookmarkRepository,
            IAnnouncementsPremiumsRefRepository announcementsPremiumsRefRepository,
            IProfileRepository profileRepository,
            IComputerRepository computerRepository,
            IComputerAnnouncementRefRepository computerAnnouncementRefRepository)
        {
            _repository = repository;
            _repository.Context = rc;

            _sectionRepository = sectionRepository;
            _sectionRepository.Context = rc;

            _categoryRepository = categoryRepository;
            _categoryRepository.Context = rc;

            _announcementImageRepository = announcementImageRepository;
            _announcementImageRepository.Context = rc;

            _subsectionRepository = subsectionRepository;
            _subsectionRepository.Context = rc;

            _cityRepository = cityRepository;
            _cityRepository.Context = rc;

            _currencyRepository = currencyRepository;
            _currencyRepository.Context = rc;

            _configRepository = configRepository;
            _configRepository.Context = rc;

            _bookmarkRepository = bookmarkRepository;
            _bookmarkRepository.Context = rc;

            _announcementsPremiumsRefRepository = announcementsPremiumsRefRepository;
            _announcementsPremiumsRefRepository.Context = rc;

            _profileRepository = profileRepository;
            _profileRepository.Context = rc;

            _computerRepository = computerRepository;
            _computerRepository.Context = rc;

            _computerAnnouncementRefRepository = computerAnnouncementRefRepository;
            _computerAnnouncementRefRepository.Context = rc;

            ViewBag.Slogan = ProjectConfiguration.Get.AnnouncementConfig.Slogan;
            ViewBag.SelectedSiteCategory = CategorySearch.Announcement;
        }


        [CustomAnnouncementAuth]
        public ActionResult Create()
        {
            var model = new Announcement();
            var userID = WebSecurity.CurrentUserId;
            if (userID != -1)
            {
                var user = _profileRepository.Read(userID);
                model.Phone = user.Phone;
                model.ContactEmail = user.Email;
                model.ContactName = user.Name;
                model.IsDisplayPhone = true;
            }
            
            PopulateSectionDropDownList(allowEmpty: true);
            PopulateCategoryDropDownList();
            PopulateCityDropDownList();
            PopulateCurrencyDropDownList();
            return View(model);
        }


        [HttpPost]
        [CustomAnnouncementAuth]
        [InitializeSimpleMembership]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Announcement model, FormCollection collection)
        {
            int sectionID = 0;
            var subSectionID = 0;

            var sec = collection["newSectionId"];
            if (!String.IsNullOrEmpty(sec) && sec.Split('.')[0] != null && sec.Split('.')[1] != null)
            {
                Int32.TryParse(sec.Split('.')[0], out sectionID);
                Int32.TryParse(sec.Split('.')[1], out subSectionID);
            }

            model.SectionId = sectionID;
            model.SubsectionId = subSectionID;
            model.CreatedAt = model.UpTime = DateTime.Now;
            model.ExpiredAt = DateTime.Now.AddDays(int.Parse(_configRepository.ReadByName("ExpiredAtAnnouncement").Value));
            model.IsActive = true;
            model.UserId = WebSecurity.CurrentUserId;
            model.ViewsCount = 0;
            model.IsDisplayPhone = true;

            if (model.SubsectionId == 0 || model.SectionId == 0)
            {
                ModelState.AddModelError("SectionId", "Необходимо указать раздел");
            }

            ModelState.Remove("IsDisplayPhone");
            if (ModelState.IsValid)
            {
                var images = collection["images[]"];
                var id = _repository.Save(model, images);
                if (id > 0 && WebSecurity.CurrentUserId == -1 && ProjectConfiguration.IsAnonymousUserAllowed)
                {
                    var compKey = Domain.Utils.FingerPrint.Value();
                    var comp = _computerRepository.GetByComputerKey(compKey);
                    int dbCompID;
                    if (comp == null)
                    {
                        dbCompID = _computerRepository.Save(new Computer
                        {
                            Key = compKey
                        });
                    }
                    else
                    {
                        dbCompID = comp.Id;
                    }
                    _computerAnnouncementRefRepository.Save(new ComputerAnnouncementRef
                    {
                        AnnouncementsId = id,
                        ComputerId = dbCompID
                    });
                }

                return RedirectToAction("Details", "Announcement", new { Id = id });
            }

            PopulateSectionDropDownList(model.SectionId + "." + model.SubsectionId);
            //int firstSectionId = PopulateSectionDropDownList(model.Section);
            //PopulateSubsectionDropDownList((model.Section != null) ? model.SectionId : firstSectionId, model.Subsection);
            PopulateCategoryDropDownList(model.Category);
            PopulateCityDropDownList(model.City);
            PopulateCurrencyDropDownList(model.Currency);
            ViewBag.UploadedImages = (collection["images[]"] != null) ? collection["images[]"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            return View(model);
        }


        [HttpGet]
        [CustomAnnouncementEditAuth]
        public ActionResult Edit(int id)
        {
            var announcement = _repository.Read(id);
           
            if (announcement == null) return HttpNotFound();

            var isAnonumousUserCanEdit = _anonymousUserService.IsUserCanEdit(id);
            if (WebSecurity.CurrentUserId != announcement.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                if (!isAnonumousUserCanEdit)
                {
                    return HttpNotFound();
                }
            }

            if(announcement.Price.HasValue && !announcement.Currency.Rate.Equals(1.0f))
            {
                announcement.Price = Math.Round((decimal)announcement.Price*(decimal) announcement.Currency.Rate, 2);
            }

            if (announcement.User.UserId != -1 && (announcement.Phone == null || announcement.Phone.Equals(string.Empty)))
            {
                announcement.Phone = announcement.User.Phone;
            }

            PopulateCategoryDropDownList(announcement.Category);
            PopulateSectionDropDownList(announcement.SectionId + "." + announcement.SubsectionId);
            //PopulateSubsectionDropDownList(announcement.SectionId, announcement.Subsection);
            PopulateCityDropDownList(announcement.City);
            PopulateCurrencyDropDownList(announcement.Currency);
            ViewBag.ImagePath = ImageProvider.PublicAnouncementImagesPath;
            ViewBag.UploadedImages = (announcement.Images != null) ? from image in announcement.Images select image.Link + ";" + image.IsTitular.ToString().ToLower() : null;

            return View(announcement);
        }


        [HttpPost]
        [CustomAnnouncementEditAuth]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Announcement model, FormCollection collection)
        {
            int sectionID = 0;
            var subSectionID = 0;

            var sec = collection["newSectionId"];
            if (!String.IsNullOrEmpty(sec) && sec.Split('.')[0] != null && sec.Split('.')[1] != null)
            {
                Int32.TryParse(sec.Split('.')[0], out sectionID);
                Int32.TryParse(sec.Split('.')[1], out subSectionID);
            }

            model.SectionId = sectionID;
            model.SubsectionId = subSectionID;
            model.ExpiredAt =
                DateTime.Now.AddDays(int.Parse(_configRepository.ReadByName("ExpiredAtAnnouncement").Value));
            model.IsActive = true;
            model.IsDisplayPhone = true;

            //TODO: Check is it need -->
            //model.UserId = (User.IsInRole("Administrator") || User.IsInRole("Moderator")) ? model.UserId : WebSecurity.CurrentUserId;

            var isAnonumousUserCanEdit = _anonymousUserService.IsUserCanEdit(model.Id);

            if (!ProjectConfiguration.IsAnonymousUserAllowed && !isAnonumousUserCanEdit)
            {
                if (model.UserId != WebSecurity.CurrentUserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
                {
                    ModelState.AddModelError("Name", "Не хватило прав доступа");
                }
            }

            if (model.SubsectionId == 0 || model.SectionId == 0)
            {
                ModelState.AddModelError("SubsectionId", "Необходимо указать подраздел");
            }

            ModelState.Remove("IsDisplayPhone");
            if (ModelState.IsValid)
            {
                var images = collection["images[]"];

                try
                {
                    int id = _repository.Save(model);
                    _announcementImageRepository.SaveManyImages(id, images);

                    return RedirectToAction("Details", "Announcement", new {Id = id});
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                    return RedirectToAction("Edit", "Announcement", new {Id = model.Id});
                }
            }

            PopulateCategoryDropDownList(model.Category);
            PopulateSectionDropDownList(model.SectionId + "." + model.SubsectionId);
            //PopulateSubsectionDropDownList(model.SectionId, model.Subsection);
            PopulateCityDropDownList(model.City);
            PopulateCurrencyDropDownList(model.Currency);
            ViewBag.UploadedImages = (collection["images[]"] != null) ? collection["images[]"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            return View(model);
        }


        [HttpGet]
        public ActionResult Details(int? id, int? commentPage, FilterParams filterParams = null)
        {
            ViewBag.FilterParams = filterParams;
            InitializeListData();

            if(!id.HasValue)
            {
                return HttpNotFound();
            }
            Announcement announcement = _repository.Read(id.Value);

            if(announcement == null)
            {
                return HttpNotFound();
            }

            var isUserCanEdit = _anonymousUserService.IsUserCanEdit(announcement.Id);

            if(!announcement.IsActive)
            {
                if (User.IsInRole("Administrator") || WebSecurity.CurrentUserId.Equals(announcement.UserId) || isUserCanEdit)
                {
                    TempData["notice"] = "Это объявление заблокировано";
                }
                else
                {
                    return HttpNotFound();
                }
            }

            ViewBag.IsUserCanEdit = isUserCanEdit;
            _repository.IncrementViewsCount(announcement);
            ViewBag.Sections = _sectionRepository.Read();
            ViewBag.Subsections = _subsectionRepository.ReadBySection(announcement.SectionId).ToArray();

            ViewBag.AnnouncementId = announcement.Id;
            ViewBag.IsIssetInBookmark = _bookmarkRepository.IsIsset(WebSecurity.CurrentUserId, announcement.Id);
            ViewBag.Comments = (announcement.Comments!=null && announcement.Comments.Count > 0) 
                ? announcement.Comments.ToPagedList(commentPage ?? 1, ProjectConfiguration.Get.CommentsOnPage) 
                : null;

            return View(announcement);
        }


        [HttpGet]
        public ActionResult List(FilterParams filterParams = null)
        {
            InitializeListData();
            ViewBag.FilterParams = filterParams ?? new FilterParams();

            var announcementsToSort = _repository.ReadActivesQuery().OrderByDescending(a => a.UpTime).AsQueryable();

            if (filterParams.SectionId.HasValue)
            {
                ViewBag.Subsections = _subsectionRepository.ReadBySection(filterParams.SectionId.Value);

                if (filterParams.SectionId.Value == 3)
                {
                    ViewBag.SelectedSiteCategory = CategorySearch.Auto;
                }

                // Premiums   TODO: Need refactoring!
                int premium1Count = int.Parse(ProjectConfiguration.Get.Configs.First(c => c.Name.Equals("CountOfPremium1Items")).Value);
                int premium2Count = int.Parse(ProjectConfiguration.Get.Configs.First(c => c.Name.Equals("CountOfPremium2Items")).Value);
                int premium3Count = int.Parse(ProjectConfiguration.Get.Configs.First(c => c.Name.Equals("CountOfPremium3Items")).Value);
                int premium1Id = ProjectConfiguration.Get.AnnouncementConfig.Premium1Id;
                int premium2Id = ProjectConfiguration.Get.AnnouncementConfig.Premium2Id;
                int premium3Id = ProjectConfiguration.Get.AnnouncementConfig.Premium3Id;

                var premium1Ads = _announcementsPremiumsRefRepository.ReadActiveByPremium(premium1Id, filterParams.SectionId.Value, premium1Count);
                ViewBag.Premium1Ads = premium1Ads;
                announcementsToSort = announcementsToSort.Except(premium1Ads);
                
                // Если это первая страница, тогда еще добавить объявления Премиум-2 и Премиум-3
                if (filterParams.CurrentPage.HasValue && filterParams.CurrentPage.Value == 1)
                {
                    var premium2Ads = _announcementsPremiumsRefRepository.ReadActiveByPremium(premium2Id, filterParams.SectionId.Value, premium2Count);
                    var premium3Ads = _announcementsPremiumsRefRepository.ReadActiveByPremium(premium3Id, filterParams.SectionId.Value, premium3Count);

                    ViewBag.Premium2Ads = premium2Ads;
                    ViewBag.Premium3Ads = premium3Ads;

                    announcementsToSort = announcementsToSort.Except(premium2Ads);
                    announcementsToSort = announcementsToSort.Except(premium3Ads);
                }
            }

            if (filterParams.IsEnabledFilter)
            {
                announcementsToSort = _repository.SortByParams(announcementsToSort, filterParams.HasPhoto, filterParams.HasAuction,
                                                         filterParams.CityId, filterParams.PriceStart, filterParams.PriceEnd);
            }

            announcementsToSort = _repository.Sort(announcementsToSort, filterParams.SortOrder, filterParams.SortOptions, filterParams.SectionId,
                                     filterParams.SubsectionId, filterParams.CategoryId);
            
            return View("List", announcementsToSort.ToPagedList((filterParams.CurrentPage.HasValue) ? filterParams.CurrentPage.Value : 1, filterParams.PageSize));
        }


        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            var announcement = _repository.Read(id);
            if (announcement == null) return HttpNotFound();

            var isAnonumousUserCanEdit = _anonymousUserService.IsUserCanEdit(id);
            if (WebSecurity.CurrentUserId != announcement.UserId && !User.IsInRole("Administrator"))
            {
                if (!isAnonumousUserCanEdit)
                {
                    return HttpNotFound();
                }
            }

            return View(announcement);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var announcement = _repository.Read(id);
            if (announcement == null) return HttpNotFound();

            var isAnonumousUserCanEdit = _anonymousUserService.IsUserCanEdit(id);
            if (WebSecurity.CurrentUserId != announcement.UserId && !User.IsInRole("Administrator"))
            {
                if (!isAnonumousUserCanEdit)
                {
                    return HttpNotFound();
                }
            }

            try
            {
                _repository.Delete(announcement);
            }
            catch(DataException de)
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("List");
        }


        /* Required AnnouncementSortModel */
        public ActionResult SortedByParamList(FilterParams filterParams)
        {
            filterParams.IsEnabledFilter = true;
            filterParams.CurrentPage = 1;
            return List(filterParams);
        }


        /**
         * Создание и инициализация SelectList'ов
         */
        #region Populate
        private void PopulateCityDropDownList(object selectedCity = null)
        {
            var citiesQuery = _cityRepository.Read().OrderBy(c => c.Id);
            ViewBag.Cities = new SelectList(citiesQuery, "Id", "Name", selectedCity);
        }

        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var categories = _categoryRepository.Read().OrderBy(c => c.Id);
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedCategory);
        }

        private void PopulateSectionDropDownList(object selectedSection = null, bool allowEmpty = false)
        {
            var result = new List<GroupedSelectList>();
            var sections = _sectionRepository.Read().OrderBy(c => c.Name).ToList();
            foreach (var section in sections)
            {
                var temp = new GroupedSelectList
                {
                    Text = section.Name,
                    Value = section.Id.ToString(),
                    Children = new List<GroupedSelectList>()
                };
                var subSection = _subsectionRepository.ReadBySection(section.Id).OrderBy(c => c.Name).ToList();
                foreach (var subsection in subSection)
                {
                    var value = section.Id + "." + subsection.Id;
                    var tempSub = new GroupedSelectList
                    {
                        Text = subsection.Name,
                        //Value = subsection.Id.ToString()
                        Value = value,
                        IsCurrent = selectedSection != null && selectedSection.ToString().Equals(value)
                    };
                    temp.Children.Add(tempSub);
                }
                result.Add(temp);
            } ViewBag.Sections = result;
        }

        private void PopulateSubsectionDropDownList(int sectiondId, object selectedSubsection)
        {
            var subsections = _subsectionRepository.ReadBySection(sectiondId).OrderBy(c => c.Name);
            ViewBag.Subsections = new SelectList(subsections, "Id", "Name", selectedSubsection);
        }

        private void PopulateCurrencyDropDownList(object selectedCurrency = null)
        {
            var currenciesQuery = _currencyRepository.Read();
            ViewBag.Currencies = new SelectList(currenciesQuery, "Id", "Name", selectedCurrency);
        }

        private void InitializeListData()
        {
            ViewBag.Sections = _sectionRepository.Read();
            ViewBag.Categories = _categoryRepository.Read();
        }
        #endregion



        [HttpPost]
        [CustomAnnouncementEditAuth]
        public ActionResult Up(int id)
        {
            var announcement = _repository.Read(id);

            if (announcement == null)
            {
                return HttpNotFound();
            }

            if ((WebSecurity.CurrentUserId == announcement.UserId 
                && announcement.UpTime <= DateTime.Now.AddHours(-int.Parse(_configRepository.ReadByName("UpTimeAnnouncement").Value)))
                || User.IsInRole("Administrator")
                || User.IsInRole("Moderator"))
            {
                announcement.UpTime = DateTime.Now;
                announcement.ExpiredAt = DateTime.Now.AddDays(int.Parse(_configRepository.ReadByName("ExpiredAtAnnouncement").Value));

                try
                {
                    _repository.SaveIgnoreCurrency(announcement);
                }
                catch(Exception)
                {
                    TempData["error"] = "Не удалось поднять объявление. Обратитесь к администратору";
                }
                return RedirectToAction("Details", new { id = id });
            }

            TempData["error"] = "Вы не можете сейчас поднять это объявление";
            return RedirectToAction("Details", new { id = id });
        }

        

        public ActionResult Catalog()
        {
            var sections = _sectionRepository.Read();
            return View(sections);
        }




        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
