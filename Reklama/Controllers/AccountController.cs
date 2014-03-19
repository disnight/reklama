using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain.Entity.Shared;
using Domain.Repository.Shared;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Reklama.Filters;
using Reklama.Models;
using Reklama.Models.ViewModels.Shared;
using WebMatrix.WebData;

namespace Reklama.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        // For line #282
        private readonly IProfileRepository _profileRepository;


        public AccountController(IProfileRepository userProfileRepository)
        {
            _profileRepository = userProfileRepository;
        }


        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
            {
                return (returnUrl != null) ? RedirectToLocal(returnUrl) : RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Неверно введел Email или пароль");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [CaptchaMvc.Attributes.CaptchaVerify("Защитный код введен неверно")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    var token = WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                                                                    requireConfirmationToken: true);
                    //WebSecurity.Login(model.Email, model.Password);
                    //RedirectToRoute("Default");
                    return  RedirectToAction("SendActivationMail", "Email", new {token = token, email = model.Email});
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль был изменен"
                : message == ManageMessageId.SetPasswordSuccess ? "Пароль установлен"
                : message == ManageMessageId.RemoveLoginSuccess ? "Пользователь успешно удален"
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        TempData["notice"] = "Пароль успешно изменён!";
                        RedirectToAction("Index", "Profile", new { id = WebSecurity.CurrentUserId });
                        //return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        TempData["error"] = "Текущий пароль неверный, или новый пароль имеет неправильный формат";
                        ModelState.AddModelError("", "Текущий пароль неверный, или новый пароль имеет неправильный формат");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        TempData["e"] = e.Message;
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            //return View(model);
            return RedirectToAction("Edit", "Profile", new { id = WebSecurity.CurrentUserId });
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { Email = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                using (ReklamaContext rc = new ReklamaContext())
                {
                    _profileRepository.Context = rc;

                    // Insert a new user into the database

                    // Check if user already exists
                    UserProfile user = _profileRepository.Read().FirstOrDefault(u => u.Email.ToLower() == model.Email.ToLower());

                    if (user == null)
                    {
                        // Insert name into the profile table
                        _profileRepository.Save(new UserProfile { Email = model.Email });

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.Email);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Такой Email уже существует. Попробуйте ввести другой");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }


        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult RememberPassword(RegisterExternalLoginModel rememberModel)
        {
            if(ModelState.IsValid)
            {
                if(!WebSecurity.UserExists(rememberModel.Email))
                {
                    ModelState.AddModelError("Email", "Пользователя с указанным адресом не существует в базе");
                    return View(rememberModel);
                }

                string token;

                try
                {
                    token = WebSecurity.GeneratePasswordResetToken(rememberModel.Email);
                }
                catch(InvalidOperationException)
                {
                    ModelState.AddModelError("Email", "Пользователя с указанным адресом не существует в базе");
                    return View(rememberModel);
                }

                var message =
                    string.Format(
                        "На сайте reklama.tm было запрошено действие восстановление пароля для вашей учетной записи\n" +
                        "Если это были вы, пожалуйста, перейдите по ссылке в течении 24 часов: {0}.\n" +
                        "Если вы не инициировали действие восстановления пароля, просто проигнорируйте это письмо\n" +
                        "Данное письмо было отправлено роботом и не требует ответа с Вашей стороны\n" +
                        "Всего наилучшего, http://reklama.tm/",
                        Url.Action("ConfirmRememberPassword", "Account", new { token = token }, "http")
                    );

                var mailRobot = new MailRobot();

                try
                {
                    mailRobot.SendMessage(rememberModel.Email, "Восстановление пароля на сайте reklama.tm", message);
                }
                catch(Exception)
                {
                    TempData["error"] = "Возникла ошибка. Пожалуйста, свяжитесь с администратором";
                    return View(rememberModel);
                }

                TempData["notice"] = "На ваш Email адрес были высланы инструкции по восстановлению пароля";
                return RedirectToAction("List", "Announcement");
            }

            return View(rememberModel);
        }


        [AllowAnonymous, HttpGet]
        public ActionResult ConfirmRememberPassword(string token)
        {
            return View((object)token);
        }


        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult ConfirmRememberPassword(string token, string newPassword)
        {
            try
            {
                var result = WebSecurity.ResetPassword(token, newPassword);

                if (!result)
                {
                    TempData["error"] = "К сожалению, возникла ошибка при попытке изменения пароля";
                }
                else
                {
                    TempData["notice"] = "Пароль был успешно изменен!";
                }
            }
            catch(Exception)
            {
                TempData["error"] = "К сожалению, возникла ошибка при попытке изменения пароля";
            }
            
            return RedirectToAction("List", "Announcement");
        }


        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Такой пользователь уже существует. Введите другой Email";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Пользователь с таким e-mail уже существует.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Введенный e-mail или пароль неверны. Повторите ввод.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Введенный e-mail или пароль неверны. Повторите ввод.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Ответ на секретный вопрос неправдоподобный.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Секретный вопрос неправдоподобный.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Введенный e-mail или пароль неверны. Повторите ввод.";

                case MembershipCreateStatus.ProviderError:
                    return "Возникла ошибка при проверке удостоверений. Пожалуйста, повторите ввод или обратитесь к администратору";

                case MembershipCreateStatus.UserRejected:
                    return "Возникла ошибка при проверке удостоверений. Пожалуйста, повторите ввод или обратитесь к администратору";

                default:
                    return "Возникла непридвиденная ошибка. Пожалуйста, повторите ввод или обратитесь к администратору";
            }
        }
        #endregion
    }
}
