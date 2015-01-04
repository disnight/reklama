using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reklama.Data.Entities;
using Reklama.Data.Servises;

namespace Reklama.Areas.CatalogAdmin.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly ShopsService _shopService = new ShopsService();
        

        public ActionResult Index()
        {
            return View(_shopService.GetManufacturers());
        }
       

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    var manufacturer = _shopService.GetManufacturer(id);
                    if (manufacturer != null && !String.IsNullOrEmpty(collection["manufacturerName"]))
                    {
                        manufacturer.Name = collection["manufacturerName"];
                        _shopService.Save();
                    }
                }
                else
                {
                    
                    _shopService.AddManufacturer(new Manufacturer { Name = collection["manufacturerName"] });
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    var manufacturer = _shopService.GetManufacturer(id);
                    if (manufacturer != null && !manufacturer.Product.Any()) 
                    {
                        _shopService.RemoveManufacturer(manufacturer);
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
