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
    public class PageController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
            _pageRepository.Context = rc;
        }


        public ActionResult Details(int id)
        {
            var page = _pageRepository.Read(id);

            if(page == null)
            {
                return HttpNotFound();
            }

            return View(page);
        }



        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
