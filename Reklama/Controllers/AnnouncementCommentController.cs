using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;
using Reklama.Filters;
using Reklama.Models.ViewModels.Announcement;
using WebMatrix.WebData;
using Reklama.Models;

namespace Reklama.Controllers
{
    public class AnnouncementCommentController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IAnnouncementCommentRepository _announcementCommentRepository;


        public AnnouncementCommentController(IAnnouncementCommentRepository announcementCommentRepository)
        {
            _announcementCommentRepository = announcementCommentRepository;
            _announcementCommentRepository.Context = rc;
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(AnnouncementCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {

                AnnouncementComment comment = new AnnouncementComment()
                                                  {
                                                      Id = commentViewModel.CommentId,
                                                      AnnouncementId = commentViewModel.AnnouncementId,
                                                      Comment = commentViewModel.Comment,
                                                      CreatedAt = DateTime.Now,
                                                      IsActive = true,
                                                      UserId = WebSecurity.CurrentUserId
                                                  };

                try
                {
                    _announcementCommentRepository.Save(comment);
                }
                catch(DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return RedirectToAction("Details", "Announcement", new { Id = comment.AnnouncementId });
            }
            else
            {
                TempData["Comment"] = commentViewModel.Comment;
                return RedirectToAction("Details", "Announcement", new { Id = commentViewModel.AnnouncementId });
            }
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int commentId)
        {
            var announcementComment = _announcementCommentRepository.Read(commentId);

            if (announcementComment == null)
            {
                return HttpNotFound();
            }

            try
            {
                _announcementCommentRepository.Delete(announcementComment);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("Details", "Announcement", new { id = announcementComment.AnnouncementId });
        }



        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
