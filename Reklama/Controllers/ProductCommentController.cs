using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Filters;
using Reklama.Models;
using Reklama.Models.ViewModels.Catalog;
using WebMatrix.WebData;

namespace Reklama.Controllers
{
    public class ProductCommentController : Controller
{
        private ReklamaContext rc = new ReklamaContext();

        private readonly IProductFeedbackRepository _productCommentRepository;


        public ProductCommentController(IProductFeedbackRepository productCommentRepository)
        {
            _productCommentRepository = productCommentRepository;
            _productCommentRepository.Context = rc;
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(ProductCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {

                var comment = new ProductFeedback()
                                                  {
                                                      Id = commentViewModel.CommentId,
                                                      ProductId = commentViewModel.ProductId,
                                                      Comment = commentViewModel.Comment,
                                                      CreatedAt = DateTime.Now,
                                                      UserId = WebSecurity.CurrentUserId
                                                  };

                try
                {
                    _productCommentRepository.Save(comment);
                }
                catch(DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return RedirectToAction("Feedbacks", "Product", new { Id = comment.ProductId });
            }
            else
            {
                TempData["Comment"] = commentViewModel.Comment;
                return RedirectToAction("Feedbacks", "Product", new { Id = commentViewModel.ProductId });
            }
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int commentId)
        {
            var announcementComment = _productCommentRepository.Read(commentId);

            if (announcementComment == null)
            {
                return HttpNotFound();
            }

            try
            {
                _productCommentRepository.Delete(announcementComment);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("Feedbacks", "Product", new { id = announcementComment.ProductId });
        }



        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
