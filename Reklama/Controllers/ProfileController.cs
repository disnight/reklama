using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Shared;
using Domain.Repository.Shared;
using Reklama.Data.Servises;
using Reklama.Filters;
using WebMatrix.WebData;
using Reklama.Models;
using Domain.Repository.Catalogs;

namespace Reklama.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private IProfileRepository _profileRepository;
        private IPrivateMessageRepository _privateMessageRepository;
        private IShopRepository _shopRepository;
        private ShopsService _shopService = new ShopsService();

        public ProfileController(IProfileRepository profileRepository, IPrivateMessageRepository privateMessageRepository, IShopRepository shopRepository)
        {
            _profileRepository = profileRepository;
            _profileRepository.Context = rc;

            _privateMessageRepository = privateMessageRepository;
            _privateMessageRepository.Context = rc;

            _shopRepository = shopRepository;
            _shopRepository.Context = rc;
        }


        //
        // GET: /Profile/5
        public ActionResult Index(int id)
        {
            UserProfile profile = _profileRepository.Read(id);

            if(profile == null)
            {
                return HttpNotFound();
            }

            ViewBag.InboxUnreadCount = _privateMessageRepository.GetUnreadCount(WebSecurity.CurrentUserId);
            if (_shopService.IsExistShopByCurrentUser(id))
            {
                ViewBag.ShopId = _shopService.GetShopByUserID(id).ID;
            }
            return View(profile);
        }

        //
        // GET: /Profile/Edit/5

        public ActionResult Edit(int id)
        {
            var profile = _profileRepository.Read(id);

            if(profile == null)
            {
                return HttpNotFound();
            }

            if(profile.UserId != WebSecurity.CurrentUserId && !User.IsInRole("Administrator"))
            {
                return RedirectToRoute("RestrictedAccess");
            }

            return View(profile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile profile)
        {

            if (profile.UserId != WebSecurity.CurrentUserId && !User.IsInRole("Administrator"))
            {
                return RedirectToRoute("RestrictedAccess");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var id = _profileRepository.Save(profile);

                    return RedirectToAction("Index", new {id = id});
                }
                catch (DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                    return RedirectToAction("Index", new {id = profile.UserId});
                }
            }

            return View(profile);
        }

        //
        // GET: /Profile/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var profile = _profileRepository.Read(id);
            return View(profile);
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var profile = _profileRepository.Read(id);

            try
            {
                _profileRepository.Delete(profile);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(profile);
            }
        }



        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
