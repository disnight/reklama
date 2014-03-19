using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Announcements;
using Domain.Entity.Articles;
using Domain.Repository.Articles;
using Reklama.Filters;
using Reklama.Models.ViewModels.Announcement;
using WebMatrix.WebData;
using Reklama.Models;

namespace Reklama.Controllers
{
    public class ArticleCommentController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IArticleCommentRepository _articleCommentRepository;

        public ArticleCommentController(IArticleCommentRepository articleCommentRepository)
        {
            _articleCommentRepository = articleCommentRepository;
            _articleCommentRepository.Context = rc;
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(AnnouncementCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {

                var comment = new ArticleComment()
                {
                    Id = commentViewModel.CommentId,
                    ArticleId = commentViewModel.AnnouncementId,
                    Comment = commentViewModel.Comment,
                    CreatedAt = DateTime.Now,
                    UserId = WebSecurity.CurrentUserId
                };

                try
                {
                    _articleCommentRepository.Save(comment);
                }
                catch (DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return RedirectToAction("Details", "Article", new { Id = comment.ArticleId });
            }

            TempData["Comment"] = commentViewModel.Comment;
            return RedirectToAction("Details", "Article", new { Id = commentViewModel.AnnouncementId });
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int commentId)
        {
            var articleComment = _articleCommentRepository.Read(commentId);

            if (articleComment == null)
            {
                return HttpNotFound();
            }

            try
            {
                _articleCommentRepository.Delete(articleComment);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("Details", "Article", new { id = articleComment.ArticleId });
        }

        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
