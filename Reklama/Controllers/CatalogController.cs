using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Catalogs;
using Domain.Enums;
using Domain.Repository.Shared;
using PagedList;
using Reklama.Data.Servises;
using Reklama.Filters;
using Reklama.Models;
using Reklama.Models.ViewModels.Catalog;
using Reklama.ViewModels.Catalog;
using WebMatrix.WebData;

namespace Reklama.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProfileRepository _profileRepository;

        public CatalogController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;

            ViewBag.SelectedSiteCategory = CategorySearch.Product;
        }

        private readonly ProductService _productService = new ProductService();
        //
        // GET: /Catalog/

        public ActionResult Index()
        {
            return View(_productService.GetAllGroups());
        }

        public ActionResult Category(ProductsFilterParams filter)
        {
            if (filter == null)
            {
                filter = new ProductsFilterParams();
            }
            var result = new CategoryOverviewViewModel();

            var products = _productService.GetProducts(filter.SecondCategoryId);
            result.Manufacturers = products.Select(q => q.Manufacturer).Distinct().OrderBy(q => q.Name);
            products = products.Sort(filter.SortOrder, filter.SortOptions, filter.SecondCategoryId, filter.ThirdCategoryId);

            result.Products = products.ToPagedList(filter.Page, filter.PageSize);
            result.Filter = filter;
            result.CurrentCategory = _productService.GetCategory(filter.SecondCategoryId);


            return View(result);
        }

        public ActionResult Details(long id)
        {
            var result = new DetailsViewModel();

            var product = _productService.GetProduct(id);

            result.CurrentProduct = product;
            result.Manufacturers = _productService.GetManufacturers(product.CategoryID);
            result.Sections = _productService.GetParameters(id).Select(q => new ParametersSectionViewModel
                {
                    Section = q.Key,
                    Parameters = q.Select(w => new ParameterViewModel
                    {
                        Name = w.CategoryParameter.Name,
                        Description = w.CategoryParameter.Description,
                        Unit = w.Unit != null ? w.Unit.Name : "",
                        Value = w.Value
                    })
                });
            return View(result);
        }

        public ActionResult Feedbacks(int id, int? commentPage)
        {

            var rc = new ReklamaContext();
                _profileRepository.Context = rc;
                var result = new ProductFeedbacksPageViewModel();
                var product = _productService.GetProduct(id);
                
                result.Product = product;
                result.Comments = product.ProductFeedback.Select(q => new FeedbackViewModel(_profileRepository)
                {
                    ID = q.ID,
                    UserID = q.UserID,
                    Comment = q.Comment,
                    CreatedAt = q.CreatedAt
                }).ToPagedList(commentPage ?? 1, ProjectConfiguration.Get.CommentsOnPage);

                return View(result);
            
        }

        public ActionResult Shops(int id, ProductsFilterParams filter = null)
        {
            if (filter == null)
            {
                filter = new ProductsFilterParams();
            }

            var result = new ProductShopsPageViewModel();
            
            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            result.Filter = filter;
            result.Product = product;
            result.Manufacturers = product.Category.Product.Select(q => q.Manufacturer).Distinct();

            var shops = product.ShopProduct.Distinct().Sort(filter.SortOrder,
                filter.SortOptions);

            result.Shops = shops.ToPagedList(filter.Page, filter.PageSize);

            return View(result);
        }

        public ActionResult Photos(int id)
        {
            var product = _productService.GetProduct(id);
            return View(product);
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult FeedbackDelete(long commentId)
        {
            var comment = _productService.GetProductFeedback(commentId);

            if (comment == null)
            {
                return HttpNotFound();
            }
            try
            {
                _productService.ProductFeedbackDelete(commentId);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }
            return RedirectToAction("Feedbacks", "Catalog", new { id = comment.ProductID });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(ProductCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productService.ProductFeedbackCreate(new Data.Entities.ProductFeedback
                    {
                        Comment = commentViewModel.Comment,
                        ProductID = commentViewModel.ProductId,
                        CreatedAt = DateTime.Now,
                        UserID = WebSecurity.CurrentUserId
                    });
                }
                catch (DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return RedirectToAction("Feedbacks", "Catalog", new { Id = commentViewModel.ProductId });
            }
            else
            {
                TempData["Comment"] = commentViewModel.Comment;
                return RedirectToAction("Feedbacks", "Catalog", new { Id = commentViewModel.ProductId });
            }
        }
    }
}
