using Domain.Entity.Admin;
using Domain.Enums;
using Domain.Repository.Announcements;
using Reklama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reklama.Models.ViewModels.Payment;
using Domain.Repository.Admin;

namespace Reklama.Controllers
{
    public class PremiumController: Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IAnnouncementsPremiumsRefRepository _announcementsPremiumsRefRepository;


        public PremiumController(IAnnouncementRepository announcementRepository,
            IAnnouncementsPremiumsRefRepository announcementsPremiumsRefRepository)
        {
            _announcementRepository = announcementRepository;
            _announcementRepository.Context = rc;

            _announcementsPremiumsRefRepository = announcementsPremiumsRefRepository;
            _announcementsPremiumsRefRepository.Context = rc;
        }


        public ActionResult Announcement(int id)
        {
            var announcement = _announcementRepository.Read(id);

            if (announcement == null)
            {
                return HttpNotFound();
            }

            return View(announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnnouncementPayment(int announcement_id, int premium_id)
        {
            Premium premium;
            var announcement = _announcementRepository.Read(announcement_id);
            int maxPremiumsCount = 1000;
            int premiumsCount = _announcementsPremiumsRefRepository.ReadActiveByPremium(premium_id, announcement.SectionId, maxPremiumsCount).Count();

            if (ProjectConfiguration.Get.AnnouncementConfig.Premium1Id == premium_id)
            {
                maxPremiumsCount = ProjectConfiguration.Get.GetConfigValue("CountOfPremium1Items").ToInt32(null);
                premium = ProjectConfiguration.Get.AnnouncementConfig.Premium1;
            }
            else if (ProjectConfiguration.Get.AnnouncementConfig.Premium2Id == premium_id)
            {
                maxPremiumsCount = ProjectConfiguration.Get.GetConfigValue("CountOfPremium2Items").ToInt32(null);
                premium = ProjectConfiguration.Get.AnnouncementConfig.Premium2;
            }
            else if (ProjectConfiguration.Get.AnnouncementConfig.Premium3Id == premium_id)
            {
                maxPremiumsCount = ProjectConfiguration.Get.GetConfigValue("CountOfPremium3Items").ToInt32(null);
                premium = ProjectConfiguration.Get.AnnouncementConfig.Premium3;
            }
            else
            {
                premium = null;
            }

            if (announcement == null || premium == null)
            {
                return HttpNotFound();
            }

            if (ProjectConfiguration.Get.Configs.First(c => c.Name.Equals("IsEnabledPaymentTerminal")).IsEnable.HasValue
                && !ProjectConfiguration.Get.Configs.First(c => c.Name.Equals("IsEnabledPaymentTerminal")).IsEnable.Value)
            {
                TempData["error"] =
                    "Платежи временно не принимаются. Приносим свои извинения за предоставленные неудобства.";
                return RedirectToAction("Announcement", new {id = announcement_id});
            }

            if (premiumsCount + 1 > maxPremiumsCount)
            {
                TempData["notice"] = string.Format("Все преимумы `{0}` заняты. Попробуйте выбрать другой!", premium.Name);
                return RedirectToAction("Announcement", new { id = announcement_id });
            }


            ViewBag.PaymentModel = new PaymentViewModel(string.Format("Payment For {0}", premium.Name), premium.Cost, 
                CategorySearch.Announcement, announcement.SectionId, announcement.Id, premium_id);
            return View(announcement);
        }


        public ActionResult PaymentSuccess(CategorySearch siteCategory, int adsPremiumsRefId)
        {
            switch (siteCategory)
            {
                case CategorySearch.Announcement:
                    var refRepository = _announcementsPremiumsRefRepository;
                    var adsPremiumsRef = refRepository.Read(adsPremiumsRefId);
                    ViewBag.AdsPremiumsRef = adsPremiumsRef;
                    break;
            }

            return View();
        }


        public ActionResult PaymentFail()
        {
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}