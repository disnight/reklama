using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Other;
using Domain.Repository.Other;
using Reklama.Models;

namespace Reklama.Controllers
{
    public class MenuController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private IMenuRepository _menuRepository;
        private IMenuPageRefRepository _menuPageRefRepository;
        private IPageRepository _pageRepository;

        public MenuController(IMenuRepository menuRepository, 
            IMenuPageRefRepository menuPageRefRepository,
            IPageRepository pageRepository)
        {
            _menuRepository = menuRepository;
            _menuRepository.Context = rc;

            _menuPageRefRepository = menuPageRefRepository;
            _menuPageRefRepository.Context = rc;

            _pageRepository = pageRepository;
            _pageRepository.Context = rc;
        }

        public ActionResult UnderLogoMenu()
        {
            var menu = _menuRepository.ReadByName("UnderLogoMenu");
            var menusPages = menu.MenuPageRefs.OrderBy(r => r.Priority);
            return View(menusPages);
        }


        public ActionResult AllsDropDownMenu()
        {
            var menu = _menuRepository.ReadByName("AllsDropDownMenu");
            var menusPages = menu.MenuPageRefs.OrderBy(r => r.Priority);
            return View(menusPages);
        }


        public ActionResult FooterMenu()
        {
            try
            {
                var menu = _menuRepository.ReadByName("FooterMenu");
                var menusPages = menu.MenuPageRefs.OrderBy(r => r.Priority);
                return View(menusPages);
            }
            catch
            {
                return null;
            }
        }


        public ActionResult FooterBottomMenu()
        {
            try
            {
                var menu = _menuRepository.ReadByName("FooterBottomMenu");
                var menusPages = menu.MenuPageRefs.OrderBy(r => r.Priority);
                return View(menusPages);
            }
            catch(Exception) {
                return null;
            }
        }


        public ActionResult ListMenus()
        {
            var menus = _menuRepository.Read();
            return View(menus);
        }


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



        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
