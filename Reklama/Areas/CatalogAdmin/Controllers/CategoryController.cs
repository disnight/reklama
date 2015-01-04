using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reklama.Areas.CatalogAdmin.Models;
using Reklama.Data.Entities;
using Reklama.Data.Servises;
namespace Reklama.Areas.CatalogAdmin.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class CategoryController : Controller
    {
        private readonly ShopsService _shopService = new ShopsService();

        public ActionResult Index()
        {
            var categories = _shopService.GetCategories().GroupBy(q => q.Group).OrderBy(q => q.Key.Name);
            return View(categories);
        }
        
        public ActionResult Edit(int id)
        {
            var groups = _shopService.GetGroups().OrderBy(q => q.Name);
            var result = new EditCategoryViewModel();
            if (id == 0)
            {
                result.Groups = new SelectList(_shopService.GetGroups().OrderBy(q => q.Name), "ID", "Name");
                result.Sections = new List<CategoryParametersSection>();
                return View(result);
            }
           
            var category = _shopService.GetCategory(id);
            if (category == null) return RedirectToAction("Index");
            result.Category = category;
            result.Groups = new SelectList(_shopService.GetGroups().OrderBy(q => q.Name), "ID", "Name", category.GroupID);
            result.Sections = category.CategoryParametersSection;
            return View(result);
        }

    [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (category.ID == 0)
                {
                    _shopService.AddCategory(category);
                    return RedirectToAction("Edit", new { id = category.ID});
                }
                _shopService.UpdateCategory(category);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Deactive(int id)
        {
            try
            {
                var category = _shopService.GetCategory(id);
                if (category == null) return RedirectToAction("Index");

                category.IsActive = !category.IsActive;
                _shopService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult IsNew(int id)
        {
            try
            {
                var category = _shopService.GetCategory(id);
                if (category == null) return RedirectToAction("Index");

                category.IsNew = !category.IsNew;
                _shopService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }



        
        public ActionResult IsPopular(int id)
        {
            try
            {
                var category = _shopService.GetCategory(id);
                if (category == null) return RedirectToAction("Index");

                category.IsPopular = !category.IsPopular;
                _shopService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        #region AJAX Methods

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult AddSection(int categoryID, string name)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                var section = new CategoryParametersSection
                {
                    CategoryID = categoryID,
                    Name = name
                };
                _shopService.AddSection(section);
                return Json(section.ID);
            }
            catch
            {
                return Json(0);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult UpdateSection(int sectionID, string name)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                var section = _shopService.GetSection(sectionID);
                section.Name = name;
                _shopService.UpdateSection(section);
                return Json("Сохранно");
            }
            catch
            {
                return Json("Что-то пошло не так");
            }
        }



        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult RemoveSection(int sectionID)
        {
            var result = new RemoveResult();
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                var section = _shopService.GetSection(sectionID);
                if (section.CategoryParameter.Any())
                {
                    result.Result = false;
                    result.Message =
                        "Секция имеет параметры. В начале нужно удалить параметры, а затем секцию.";
                    return Json(result);
                }
                _shopService.RemoveSection(section);
                result.Result = true;
                result.Message = "Удалено";
                return Json(result);
            }
            catch
            {
                result.Result = false;
                result.Message = "Что-то пошло не так";
                return Json(result);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult AddParametr(int sectionID, string name, string desc)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                var param = new CategoryParameter
                {
                    SectionID = sectionID,
                    Name = name,
                    Description = desc
                };
                _shopService.AddParametr(param);
                return Json(param.ID);
            }
            catch
            {
                return Json(0);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult UpdateParametr(int paramID, string name, string desc)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                var param = _shopService.GetParametr(paramID);
                param.Description = desc;
                param.Name = name;
                _shopService.UpdateParametr(param);
                return Json("Сохранно");
            }
            catch
            {
                return Json("Что-то пошло не так");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator, Shop")]
        public JsonResult RemoveParametr(int paramID)
        {
            var result = new RemoveResult();
            if (!Request.IsAjaxRequest())
            {
                throw new InvalidCastException("Not an ajax request");
            }

            try
            {
                
                var param = _shopService.GetParametr(paramID);
                if (param.ProductParameterValue.Any())
                {
                    result.Result = false;
                    result.Message =
                        "Параметр имеет связанные данные. Для удаления этого параметра обратитесь к администратору сайта";
                    return Json(result);
                }
                _shopService.RemoveParametr(param);
                result.Result = true;
                result.Message =
                    "Удалено";
                return Json(result);
            }
            catch
            {
                result.Result = false;
                result.Message = "Что-то пошло не так";
                return Json(result);
            }
        }

        private class RemoveResult
        {
            public string Message { get; set; }
            public bool Result { get; set; }
        }

        #endregion
    }
}
