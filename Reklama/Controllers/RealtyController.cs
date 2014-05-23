using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Common;
using Domain.Entity.Realty;
using Domain.Entity.Shared;
using Domain.Repository.Admin;
using Domain.Repository.Other;
using Domain.Repository.Realty;
using Domain.Repository.Shared;
using System.IO;
using Reklama.Attributes;
using Reklama.Core.UploadImages;
using Reklama.Models.SortModels;
using Reklama.Services;
using WebMatrix.WebData;
using PagedList;
using Reklama.Models;
using Reklama.Models.ViewModels.Realty;
using System.Data;
using Domain.Enums;

namespace Reklama.Controllers
{
    public class RealtyController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        //private readonly IFileService _fileService = new FileServiceStub();
        private readonly IRealtyRepository _realtyRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRealtyCategoryRepository _categoryRepository;
        private readonly IRealtySectionRepository _sectionRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IRealtyPhotoRepository _photoRepository;
        private readonly IRealtyBookmarkRepository _bookmarkRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IComputerRepository _computerRepository;
        private readonly IComputerRealtyRefRepository _computerRealtyRefRepository;
        private readonly AnonymousUserService _anonymousUserService = new AnonymousUserService();

        public RealtyController(IRealtyRepository realtyRepository, ICityRepository cityRepository,
                                IRealtyCategoryRepository categoryRepository, IRealtySectionRepository sectionRepository,
                                ICurrencyRepository currencyRepository, IRealtyPhotoRepository photoRepository,
                                IRealtyBookmarkRepository bookmarkRepository, IProfileRepository profileRepository, IComputerRepository computerRepository, IComputerRealtyRefRepository computerRealtyRefRepository)
        {
            _realtyRepository = realtyRepository;
            _realtyRepository.Context = rc;

            _cityRepository = cityRepository;
            _cityRepository.Context = rc;

            _categoryRepository = categoryRepository;
            _categoryRepository.Context = rc;

            _sectionRepository = sectionRepository;
            _sectionRepository.Context = rc;

            _currencyRepository = currencyRepository;
            _currencyRepository.Context = rc;

            _photoRepository = photoRepository;
            _photoRepository.Context = rc;

            _bookmarkRepository = bookmarkRepository;
            _bookmarkRepository.Context = rc;

            _profileRepository = profileRepository;
            _profileRepository.Context = rc;

            _computerRepository = computerRepository;
            _computerRepository.Context = rc;

            _computerRealtyRefRepository = computerRealtyRefRepository;
            _computerRealtyRefRepository.Context = rc;

            ViewBag.SelectedSiteCategory = CategorySearch.Realty;
        }

        public ActionResult RedirectToSubdomain(string actionName, string id)
        {
            var url = "http://jay.reklama.tm/";
            url += actionName ?? "";
            url += id == null ? "" : "/" + id;
            return Redirect(url);
        }

        public ActionResult List(RealtySortByParams sortModel = null)
        {
            var realty = _realtyRepository.Read();
            ViewBag.Categories = _categoryRepository.Read();
            ViewBag.Cities = _cityRepository.Read();
            ViewBag.Sections = _sectionRepository.Read();
            ViewBag.SortModel = sortModel ?? new RealtySortByParams();
            if (sortModel != null && sortModel.IsEnableSort)
            {
                realty = _realtyRepository.SortByParams(realty, sortModel.CategoryId, sortModel.CityId, sortModel.FromPrice, sortModel.ToPrice, sortModel.CountsRoom,
                                                         sortModel.FromSquare, sortModel.ToSquare, sortModel.FromFloorCount, sortModel.ToFloorCount, sortModel.FromFloor,
                                                         sortModel.ToFloor, sortModel.FromCeillingHeight, sortModel.ToCeillingHeight, sortModel.WithPhoto,
                                                         sortModel.IsAuction, sortModel.IsPerson, sortModel.WithGarage, sortModel.WithGarden, sortModel.WithExtension,
                                                         sortModel.WithBasement, sortModel.Street);
            }
            realty = _realtyRepository.Sort(realty, sortModel.SortOrder, sortModel.SortOptions, sortModel.SectionId,
                                                 sortModel.CategoryId);

            return View("List", realty.ToPagedList(sortModel.CurrentPage.HasValue ? sortModel.CurrentPage.Value : 1, sortModel.PageSize));
        }

        public ActionResult SortedByParamList(RealtySortByParams sortModel)
        {
            sortModel.IsEnableSort = true;
            sortModel.CurrentPage = 1;
            return List(sortModel);
        }

        //
        // GET: /Realty/Details/5

        public ActionResult Details(int? id, int? commentPage, RealtySortByParams sortModel = null)
        {
            ViewBag.SortModel = sortModel;
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var realty = _realtyRepository.Read(id.Value);

            if (realty == null)
            {
                return HttpNotFound();
            }

            ViewBag.IsUserCanEditRealty = _anonymousUserService.IsUserCanEditRealty(realty.Id);
            realty.Views++;
            _realtyRepository.SaveIgnoreCurrency(realty);
            ViewBag.RealtyPhotos = _photoRepository.ReadByRealty(realty.Id).ToArray();
            ViewBag.UpTimeHours = int.Parse(ProjectConfiguration.Get.GetConfigValue("UpTimeRealty").ToString());
            ViewBag.SortModel = sortModel;
            ViewBag.Categories = _categoryRepository.Read();
            ViewBag.RealtyId = realty.Id;
            ViewBag.IsIssetInBookmark = _bookmarkRepository.IsIsset(WebSecurity.CurrentUserId, realty.Id);
            ViewBag.Comments = (realty.Comments != null && realty.Comments.Count > 0)
                ? realty.Comments.ToPagedList(commentPage ?? 1, ProjectConfiguration.Get.CommentsOnPage)
                : null;

            return View(realty);
        }


        [CustomRealtyAuth]
        public ActionResult Create()
        {
            //if(!User.Identity.IsAuthenticated)
            //{
            //    return Redirect("http://reklama.tm/Account/Login?ReturnUrl=/Realty/Create");
            //}
            var model = new Realty();
            var userID = WebSecurity.CurrentUserId;
            if (userID != -1)
            {
                var user = _profileRepository.Read(WebSecurity.CurrentUserId);
                model.Phone = user.Phone;
                model.ContactEmail = user.Email;
                model.ContactName = user.Name;
                model.IsDisplayPhone = true;
            }

            ViewBag.Cities = _cityRepository.Read();
            ViewBag.Sections = _sectionRepository.Read();
            ViewBag.Categories = _categoryRepository.Read();
            ViewBag.Currencies = _currencyRepository.Read();
            ViewBag.ExpiredAt = int.Parse(ProjectConfiguration.Get.GetConfigValue("ExpiredAtRealty").ToString());
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Realty realty, FormCollection collection)
        {
            if (realty.Floor > realty.FloorCount)
            {
                ModelState.AddModelError("Floor", "Значени поля 'На каком этаже' не может превышать общего кол-ва этажей в доме");
            }
            if (realty.IsAgency == true && realty.AgencyName.Length == 0)
            {
                ModelState.AddModelError("AgencyName", "Поле 'Название агенства' обязательно для заполнения");
            }
            //ViewBag.cError = "ModelState.IsValid = ";
            ModelState.Remove("IsDisplayPhone");
            if (ModelState.IsValid)
            {
                //ViewBag.cError += "true; ";
                realty.CreatedAt = realty.UpTime = DateTime.Now;
                realty.ExpiredAt = DateTime.Now.AddDays(int.Parse(ProjectConfiguration.Get.GetConfigValue("ExpiredAtRealty").ToString()));
                realty.UserId = WebSecurity.CurrentUserId;
                realty.Views = 0;
                realty.IsActive = true;
                realty.IsDisplayPhone = true;
                var images = collection["images[]"];
                //ViewBag.cError += "Pre Save; ";
                int id = _realtyRepository.Save(realty, images);
                //ViewBag.cError += "Save complite; id = " + id;
                if (id > 0 && WebSecurity.CurrentUserId == -1 && ProjectConfiguration.IsAnonymousUserAllowed)
                {
                    //ViewBag.cError += "Save comp init; ";

                    if (System.Web.HttpContext.Current.Request.Cookies["realties"] == null)
                    {
                        var newCookie = new HttpCookie("realties", id.ToString())
                        {
                            Expires = DateTime.Now.AddYears(1),
                            Domain = ".reklama.tm"
                        };
                        HttpContext.Response.Cookies.Add(newCookie);
                    }
                    else
                    {
                        var cookie = System.Web.HttpContext.Current.Request.Cookies["realties"];
                        cookie.Value += "," + id;
                        cookie.Expires = DateTime.Now.AddYears(1);
                        System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                    }

                    //var compKey = Domain.Utils.FingerPrint.Value();
                    //var comp = _computerRepository.GetByComputerKey(compKey);
                    //int dbCompID;
                    //if (comp == null)
                    //{
                    //    dbCompID = _computerRepository.Save(new Computer
                    //    {
                    //        Key = compKey
                    //    });
                    //}
                    //else
                    //{
                    //    dbCompID = comp.Id;
                    //}
                    //_computerRealtyRefRepository.Save(new ComputerRealtyRef
                    //{
                    //    RealtyId = id,
                    //    ComputerId = dbCompID
                    //});
                }

                //return RedirectToAction("Details", "Realty", new { Id = id });
                return Redirect("http://jay.reklama.tm/Details/" + id);
            }
            ViewBag.Cities = _cityRepository.Read();
            ViewBag.Sections = _sectionRepository.Read();
            ViewBag.Categories = _categoryRepository.Read();
            ViewBag.Currencies = _currencyRepository.Read();
            ViewBag.UploadedImages = (collection["images[]"] != null) ? collection["images[]"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            return View(realty);
        }

        //[HttpPost]
        //public ActionResult Preview(Realty realty, string images)
        //{
        //    if (images != null)
        //    {
        //        ViewBag.Photos = images.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    }
        //    ViewBag.CityName = _cityRepository.Read(realty.CityId);
        //    ViewBag.SectionName = _sectionRepository.Read(realty.SectionId);
        //    ViewBag.CurrencyName = _currencyRepository.Read(realty.CurrencyId);
        //    return PartialView("_RealtyPreview", realty);
        //}

        [HttpPost]
        [CustomRealtyEditAuth]
        public ActionResult Up(int id)
        {
            var realty = _realtyRepository.Read(id);

            if (realty == null)
            {
                return HttpNotFound();
            }
            var isRealtyUserCanEdit = _anonymousUserService.IsUserCanEditRealty(id);
            if (((WebSecurity.CurrentUserId == realty.UserId || isRealtyUserCanEdit) && realty.UpTime <= DateTime.Now.AddHours(-int.Parse(ProjectConfiguration.Get.GetConfigValue("UpTimeRealty").ToString()))
                || User.IsInRole("Administrator")
                || User.IsInRole("Moderator")))
            {
                realty.UpTime = DateTime.Now;
                realty.ExpiredAt = DateTime.Now.AddDays(int.Parse(ProjectConfiguration.Get.GetConfigValue("ExpiredAtRealty").ToString()));
                try
                {
                    _realtyRepository.SaveIgnoreCurrency(realty);
                }
                catch (Exception)
                {
                    TempData["error"] = "Не удалось поднять объявление. Обратитесь к администратору";
                }
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Details", new { id = id });
            }
        }

        [HttpGet]
        [CustomRealtyEditAuth]
        public ActionResult Edit(int id)
        {
            var realty = _realtyRepository.Read(id);
            if (realty == null) return HttpNotFound();

            var isRealtyUserCanEdit = _anonymousUserService.IsUserCanEditRealty(id);
            if (WebSecurity.CurrentUserId != realty.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                if (!isRealtyUserCanEdit)
                {
                    return HttpNotFound();
                }
                
            }

            if (realty.Price.HasValue && !realty.Currency.Rate.Equals(1.0f))
            {
                realty.Price = Math.Round((decimal)realty.Price * (decimal)realty.Currency.Rate, 2);
            }

            if (realty.UserId != -1 && (realty.Phone == null || realty.Phone.Equals(string.Empty)))
            {
                realty.Phone = realty.UserProfile.Phone;
            }

            ViewBag.Cities = _cityRepository.Read();
            ViewBag.Sections = _sectionRepository.Read();
            ViewBag.Categories = _categoryRepository.Read();
            ViewBag.Currencies = _currencyRepository.Read();
            ViewBag.ImagePath = ImageProvider.PublicRealtyImagesPath;
            ViewBag.UploadedImages = from photo in realty.Photos select photo.Link + ";" + photo.IsTitular.ToString().ToLower();

            return View(realty);
        }


        [HttpPost]
        [CustomRealtyEditAuth]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Realty model, FormCollection collection)
        {
            var realty = _realtyRepository.Read(model.Id);
            if (realty == null) return HttpNotFound();

            var isRealtyUserCanEdit = _anonymousUserService.IsUserCanEditRealty(model.Id);
            if (WebSecurity.CurrentUserId != realty.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                if (!isRealtyUserCanEdit)
                {
                    return HttpNotFound();
                }
            }

            model.ExpiredAt = DateTime.Now.AddDays(int.Parse(ProjectConfiguration.Get.GetConfigValue("ExpiredAtRealty").ToString()));
            model.IsActive = true;
            model.UserId = realty.UserId;
            model.UpTime = realty.UpTime;
            model.Views = realty.Views;
            model.CreatedAt = realty.CreatedAt;
            model.IsDisplayPhone = true;

            ModelState.Remove("IsDisplayPhone");
            if (ModelState.IsValid)
            {
                var images = collection["images[]"];

                try
                {
                    int id = _realtyRepository.Save(model);
                    _photoRepository.SaveManyImages(id, images);

                    //return RedirectToAction("Details", "Realty", new { Id = id });
                    return Redirect("http://jay.reklama.tm/Details/" + id);
                }
                catch
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                    return RedirectToAction("Edit", "Realty", new { Id = model.Id });
                }
            }

            ViewBag.Cities = _cityRepository.Read();
            ViewBag.Sections = _sectionRepository.Read();
            ViewBag.Categories = _categoryRepository.Read();
            ViewBag.Currencies = _currencyRepository.Read();
            ViewBag.UploadedImages = (collection["images[]"] != null) ? collection["images[]"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            var realty = _realtyRepository.Read(id);
            if (realty == null) return HttpNotFound();

            var isRealtyUserCanEdit = _anonymousUserService.IsUserCanEditRealty(id);
            if (WebSecurity.CurrentUserId != realty.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                if (!isRealtyUserCanEdit)
                {
                    return HttpNotFound();
                }
            }

            return View(realty);
        }

        public ActionResult AddToBookmarks(CategorySearchSortModel model)
        {
            var status = "success";

            try
            {

                _bookmarkRepository.Save(new RealtyBookmark()
                                                    {
                                                        RealtyId = model.Id,
                                                        UserId = WebSecurity.CurrentUserId
                                                    });
            }
            catch
            {
                status = "fail";
            }

            return Json(new { status = status }, "text/html");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var realty = _realtyRepository.Read(id);
            if (realty == null) return HttpNotFound();

            var isRealtyUserCanEdit = _anonymousUserService.IsUserCanEditRealty(id);
            if (WebSecurity.CurrentUserId != realty.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                if (!isRealtyUserCanEdit)
                {
                    return HttpNotFound();
                }
            }

            try
            {
                _realtyRepository.Delete(realty);
            }
            catch (DataException de)
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
