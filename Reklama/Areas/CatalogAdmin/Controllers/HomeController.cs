using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reklama.Areas.CatalogAdmin.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class HomeController : Controller
    {
        //
        // GET: /CatalogAdmin/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
