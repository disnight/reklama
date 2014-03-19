using Domain.Entity.Admin;
using Domain.Enums;
using Domain.Repository.Admin;
using Reklama.Models;
using Reklama.Models.Announcements;
using Reklama.Models.ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reklama.Controllers
{
    public class PaymentController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IPaymentRepository _paymentRepository;
        private readonly IPremiumRepository _premiumRepository;
        private readonly IAnnouncementsPremiumsRefRepository _announcementsPremiumsRefRepository;


        public PaymentController(IPaymentRepository paymentRepository, IPremiumRepository premiumRepository,
            IAnnouncementsPremiumsRefRepository announcementsPremiumsRefRepository)
        {
            _paymentRepository = paymentRepository;
            _paymentRepository.Context = rc;

            _premiumRepository = premiumRepository;
            _premiumRepository.Context = rc;

            _announcementsPremiumsRefRepository = announcementsPremiumsRefRepository;
            _announcementsPremiumsRefRepository.Context = rc;
        }


        [HttpPost]
        public string Result(decimal OutSum, int InvId, string SignatureValue, int ShpAdId, int ShpPremiumId, int ShpSectionId, CategorySearch ShpSiteCategory)
        {
            var paymentModel = new PaymentViewModel(OutSum, InvId, ShpAdId, ShpPremiumId, ShpSectionId, ShpSiteCategory);
            var hash = paymentModel.GenerateHash2().Trim().ToUpper();
            SignatureValue = SignatureValue.Trim().ToUpper();

            var payment = new Payment()
            {
                AdId = ShpAdId,
                Cost = OutSum,
                CreatedAt = DateTime.Now,
                PaysystemInvId = InvId,
                SiteCategory = ShpSiteCategory,
                PremiumId = ShpPremiumId,
                SectionId = ShpSectionId
            };

            if (!hash.Equals(SignatureValue))
            {
                payment.Status = PaymentStatus.ErrorBySign;
                _paymentRepository.Save(payment);

                return "oops";
            }

            var payedPremium = _premiumRepository.Read(ShpPremiumId);
            if (payedPremium.Cost != OutSum)
            {
                payment.Status = PaymentStatus.ErrorBySum;


                _paymentRepository.Save(payment);

                return "oops by sum";
            }

            payment.Status = PaymentStatus.Success;
            _paymentRepository.Save(payment);

            var premium = _premiumRepository.Read(ShpPremiumId);

            switch (ShpSiteCategory)
            {
                case CategorySearch.Announcement:
                    var adsPremiumsRepository = _announcementsPremiumsRefRepository;
                    var adsPremiumsRef = new AnnouncementsPremiumsRef();
                    adsPremiumsRef.AdId = ShpAdId;
                    adsPremiumsRef.AdSectionId = ShpSectionId;
                    adsPremiumsRef.CreatedAt = DateTime.Now;
                    adsPremiumsRef.ExpiresAt = DateTime.Now.AddHours(premium.Lifetime);
                    adsPremiumsRef.PremiumId = ShpPremiumId;

                    try
                    {
                        adsPremiumsRepository.Save(adsPremiumsRef);
                    }
                    catch (Exception e)
                    {
                        return "oops by database";
                    }

                    break;


                case CategorySearch.Realty:
                    // TODO: Paste a realtyPremiumsRef repository here

                    break;


                default:
                    return "oops local repository";
                    break;
            }

            return string.Format("OK{0}", InvId);
        }


        [HttpPost]
        public ActionResult Success(decimal OutSum, int InvId, string SignatureValue,
            string Culture, int ShpAdId, int ShpPremiumId, int ShpSectionId, CategorySearch ShpSiteCategory)
        {
            if (ShpSiteCategory == CategorySearch.Announcement)
            {
                var adsPremiumsRef = _announcementsPremiumsRefRepository.Read()
                    .Where(a => a.AdId == ShpAdId && a.AdSectionId == ShpSectionId && a.AdSectionId == ShpSectionId && a.PremiumId == ShpPremiumId)
                    .OrderByDescending(a => a.Id)
                    .First();

                return RedirectToAction("PaymentSuccess", "Premium", new { siteCategory = ShpSiteCategory, adsPremiumsRefId = adsPremiumsRef.Id });
            }


            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult Fail(decimal OutSum, int InvId, string Culture, int ShpAdId, 
            int ShpPremiumId, int ShpSectionId, CategorySearch ShpSiteCategory)
        {
            return RedirectToAction("PaymentFail", "Premium");
        }





        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
