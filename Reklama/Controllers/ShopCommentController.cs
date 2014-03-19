using Reklama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Catalogs;
using Domain.Repository.Catalogs;
using Reklama.Filters;
using Reklama.Models.ViewModels.Catalog;
using WebMatrix.WebData;
using System.Data;

namespace Reklama.Controllers
{
    public class ShopCommentController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IShopCommentRepository _shopCommentRepository;


        public ShopCommentController(IShopCommentRepository shopCommentRepository)
        {
            _shopCommentRepository = shopCommentRepository;
            _shopCommentRepository.Context = rc;
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(ShopCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {

                ShopComment comment = new ShopComment()
                                                  {
                                                      Id = commentViewModel.CommentId,
                                                      ShopId = commentViewModel.ShopId,
                                                      Comment = commentViewModel.Comment,
                                                      CreatedAt = DateTime.Now,
                                                      IsActive = true,
                                                      UserId = WebSecurity.CurrentUserId
                                                  };

                try
                {
                    _shopCommentRepository.Save(comment);
                }
                catch(DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return RedirectToAction("Details", "Shop", new { Id = comment.ShopId });
            }
            else
            {
                TempData["Comment"] = commentViewModel.Comment;
                return RedirectToAction("Details", "Shop", new { Id = commentViewModel.ShopId });
            }
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int commentId)
        {
            var shopComment = _shopCommentRepository.Read(commentId);

            if (shopComment == null)
            {
                return HttpNotFound();
            }

            try
            {
                _shopCommentRepository.Delete(shopComment);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("Details", "Shop", new { id = shopComment.ShopId });
        }


        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
