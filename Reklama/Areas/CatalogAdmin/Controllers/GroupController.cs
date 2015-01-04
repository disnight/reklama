using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reklama.Data.Entities;
using Reklama.Data.Servises;
using Reklama.Models;

namespace Reklama.Areas.CatalogAdmin.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class GroupController : Controller
    {
        private readonly ShopsService _shopService = new ShopsService();


        //
        // GET: /CatalogAdmin/Group/

        public ActionResult Index()
        {
            return View(_shopService.GetGroups());
        }
       
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    var group = _shopService.GetGroup(id);
                    if (group != null && !String.IsNullOrEmpty(collection["groupName"]))
                    {
                        group.Name = collection["groupName"];
                        _shopService.Save();
                    }
                }
                else
                {
                    _shopService.AddGroups(new Group {Name = collection["groupName"]});
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    var group = _shopService.GetGroup(id);
                    if (group != null)
                    {
                        group.IsDeleted = true;
                        _shopService.Save();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
