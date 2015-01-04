using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain.Repository.Shared;
using Reklama.Data.Servises;
using Reklama.Models;
using Reklama.Models.Shared;

namespace Reklama.Areas.CatalogAdmin.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopsService _shopService = new ShopsService();
        private readonly IProfileRepository _profileRepository = new ProfileModel();

        public ActionResult Index()
        {
            return View(_shopService.GetShops());
        }

        [HttpPost]
        public ActionResult Deactive(int id)
        {
            using (ReklamaContext rc = new ReklamaContext())
            {
                _profileRepository.Context = rc;
                try
                {
                    var shop = _shopService.GetShop(id);
                    if (shop == null) return RedirectToAction("Index");

                    shop.IsActive = !shop.IsActive;
                    var user = _profileRepository.Read(shop.UserID.Value);

                    if (user != null)
                    {
                        if (shop.IsActive)
                        {
                            if (!Roles.IsUserInRole(user.Email, "Shop"))
                            {
                                Roles.AddUserToRole(user.Email, "Shop");
                            }
                            
                        }
                        else
                        {
                            if(Roles.IsUserInRole(user.Email, "Shop"))
                            {
                                Roles.RemoveUserFromRole(user.Email, "Shop");
                            }
                        }
                    }

                    _shopService.Save();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
        }
        
    }
}
