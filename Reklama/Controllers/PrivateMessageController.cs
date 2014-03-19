using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Shared;
using Domain.Repository.Shared;
using Reklama.Models.ViewModels.Shared;
using WebMatrix.WebData;
using Reklama.Models;
using PagedList;

namespace Reklama.Controllers
{
    [Authorize]
    public class PrivateMessageController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private IPrivateMessageRepository _privateMessageRepository;
        private IProfileRepository _profileRepository;

        public PrivateMessageController(IPrivateMessageRepository privateMessageRepository,
            IProfileRepository profileRepository)
        {
            _privateMessageRepository = privateMessageRepository;
            _privateMessageRepository.Context = rc;

            _profileRepository = profileRepository;
            _profileRepository.Context = rc;

            ViewBag.InboxUnreadCount = _privateMessageRepository.GetUnreadCount(WebSecurity.CurrentUserId);
        }

        //
        // GET: /PrivateMessage/

        public ActionResult Index(int? page)
        {
            var messages = _privateMessageRepository.ReadByRecepient(WebSecurity.CurrentUserId);

            return View(messages.ToPagedList( (page.HasValue) ? page.Value : 1, ProjectConfiguration.Get.PrivateMessagesOnPage));
        }


        public ActionResult Outbox(int? page)
        {
            var messages = _privateMessageRepository.ReadBySender(WebSecurity.CurrentUserId);
            return View(messages.ToPagedList((page.HasValue) ? page.Value : 1, ProjectConfiguration.Get.PrivateMessagesOnPage));
        }


        //
        // GET: /PrivateMessage/Details

        public ActionResult Details(int id)
        {
            var message = _privateMessageRepository.Read(id);

            if(message == null)
            {
                return HttpNotFound();
            }

            if(message.RecepientId != WebSecurity.CurrentUserId)
            {
                return RedirectToRoute("RestrictedAccess");
            }

            return View(message);
        }


        public ActionResult OutboxDetails(int id)
        {
            var message = _privateMessageRepository.Read().First(m => m.Id == id);

            if (message == null)
            {
                return HttpNotFound();
            }

            if (message.SenderId != WebSecurity.CurrentUserId)
            {
                return RedirectToRoute("RestrictedAccess");
            }

            return View(message);
        }


        public ActionResult Create(int recepientId)
        {
            var recepient = _profileRepository.Read(recepientId);

            if(recepient == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PrivateMessageViewModel()
                                {
                                    RecepientId = recepientId,
                                    Recepient = recepient.Email,
                                    Sender = User.Identity.Name
                                };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PrivateMessageViewModel model)
        {
            if(ModelState.IsValid)
            {
                var message = new PrivateMessage()
                                  {
                                      RecepientId = model.RecepientId,
                                      SenderId = WebSecurity.CurrentUserId,
                                      Subject = model.Subject,
                                      Message = model.Message
                                  };

                try
                {
                    _privateMessageRepository.Save(message);
                }
                catch(DataException)
                {
                    TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
                }

                TempData["notice"] = string.Format("Сообщение пользователю {0} успешно отправлено!", model.Recepient);
                return RedirectToAction("Index", "Profile", new {id = model.RecepientId});
            }

            return View(model);
        }


        public ActionResult CountMyUnreadMessages()
        {
            var count = _privateMessageRepository.ReadByRecepient(WebSecurity.CurrentUserId).Count(m => !m.IsRead);
            return View(count);
        }



        public ActionResult Delete(int id)
        {
            var message = _privateMessageRepository.Read(id);

            if (message == null || message.SenderId != WebSecurity.CurrentUserId)
            {
                return HttpNotFound();
            }

            try
            {
                _privateMessageRepository.Delete(message);
            }
            catch
            {
                TempData["error"] = ProjectConfiguration.Get.DataErrorMessage;
            }

            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}
