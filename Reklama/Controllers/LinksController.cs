using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Admin;
using Domain.Repository.Admin;
using Domain.Repository.Other;
using Reklama.Models;


namespace Reklama.Controllers
{
    public class LinksController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IConfigRepository _configRepository;
        private readonly IPageRepository _pageRepository;

        public LinksController(IConfigRepository configRepository, IPageRepository pageRepository)
        {
            _configRepository = configRepository;
            _configRepository.Context = rc;

            _pageRepository = pageRepository;
            _pageRepository.Context = rc;
        }

        public ActionResult ViewSocialLinks()
        {
            ViewBag.Facebook = _configRepository.ReadByName("LinkFacebook").Value;
            ViewBag.Vk = _configRepository.ReadByName("LinkVk").Value;
            ViewBag.Twitter = _configRepository.ReadByName("LinkTwitter").Value;
            ViewBag.GooglePlus = _configRepository.ReadByName("LinkGooglePlus").Value;
            ViewBag.Odnoklassniki = _configRepository.ReadByName("LinkOdnoklassniki").Value;
            ViewBag.Mail = _configRepository.ReadByName("LinkMail").Value;
            return View();
        }


        /**
         * Отображает ссылку на одну из основных страниц, указанных в конфиге
         * (например, Пользовательское соглашение)
         */
        public ActionResult LinkToOneOfMainConfigPage(string mainPageName, string target = "_self", string cssClass = null)
        {
            try
            {
                var config = _configRepository.ReadByName(mainPageName);
                ViewBag.Page = _pageRepository.Read(Convert.ToInt32(config.Value));
                ViewBag.CssClass = cssClass;
                ViewBag.Target = target;
                return View(config);
            }
            catch
            {
                return null;
            }
        }


        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
