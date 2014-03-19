using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reklama.Models;
using Domain.Repository.Realty;
using Reklama.Filters;
using Reklama.Models.ViewModels.Realty;
using Domain.Entity.Realty;
using WebMatrix.WebData;
using System.Data;

namespace Reklama.Controllers
{
    public class RealtyCommentController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IRealtyCommentRepository _realtyCommentRepository;


        public RealtyCommentController(IRealtyCommentRepository realtyCommentRepository)
        {
            _realtyCommentRepository = realtyCommentRepository;
            _realtyCommentRepository.Context = rc;
        }


        [HttpPost]
        [Authorize]
        [InitializeSimpleMembership]
        public ActionResult Create(RealtyCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {

                RealtyComment comment = new RealtyComment()
                                                  {
                                                      Id = commentViewModel.CommentId,
                                                      RealtyId = commentViewModel.RealtyId,
                                                      Comment = commentViewModel.Comment,
                                                      CreatedAt = DateTime.Now,
                                                      IsActive = true,
                                                      UserId = WebSecurity.CurrentUserId
                                                  };

                try
                {
                    _realtyCommentRepository.Save(comment);
                }
                catch(DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                return Redirect(string.Format("http://jay.reklama.tm/Details/{0}", commentViewModel.RealtyId));
            }
            else
            {
                TempData["Comment"] = commentViewModel.Comment;
                return Redirect(string.Format("http://jay.reklama.tm/Details/{0}", commentViewModel.RealtyId));
            }
        }


        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int commentId)
        {
            var realtyComment = _realtyCommentRepository.Read(commentId);

            if (realtyComment == null)
            {
                return HttpNotFound();
            }

            try
            {
                _realtyCommentRepository.Delete(realtyComment);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return Redirect(string.Format("http://jay.reklama.tm/Details/{0}", realtyComment.RealtyId));
        }


        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }

    }
}
