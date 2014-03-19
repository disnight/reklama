using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;
using WebMatrix.WebData;
using MailMessage = System.Net.Mail.MailMessage;
using MailPriority = System.Net.Mail.MailPriority;

namespace Reklama.Controllers
{
    public class EmailController : Controller
    {

        public ActionResult SendActivationMail(string token, string email)
        {
            if(token == null || email == null)
                return HttpNotFound();
            var client = new SmtpClient("localhost");
            client.Credentials = new System.Net.NetworkCredential(ProjectConfiguration.Get.LoginEmail, ProjectConfiguration.Get.PasswordEmail);
            client.Port = ProjectConfiguration.Get.PortEmail;
            client.Host = ProjectConfiguration.Get.HostEmail;
            var message = new MailMessage();
            message.Subject = "Активация пользователя на reklama.tm";
            message.IsBodyHtml = true;
            message.Body = "Данное письмо отправлено почтовым роботом сервера и не требует ответа. <br /><br /> Чтобы подтвердить свою регистрацию и начать пользоваться сервисом, пожалуйста, перейдите по этой ссылке:<br /><br /> <a href=\"http://reklama.tm/Email/Activation/?token=" + token + "\"> http://reklama.tm/Email/Activation/?token=" + token + "</a>";
            message.To.Add(email);
            message.Sender = new MailAddress("admin@reklama.tm");
            message.From = new MailAddress("admin@reklama.tm"/*ProjectConfiguration.Get.Email*/);
            message.Headers.Add("from","admin@reklama.tm");
            message.Priority = MailPriority.Normal;
            client.EnableSsl = true;
            message.SubjectEncoding = UTF8Encoding.UTF8;
            message.BodyEncoding = UTF8Encoding.UTF8;
            try
            {
               client.Send(message);
            }
            catch
            {
                TempData["notice"] = ProjectConfiguration.Get.DataErrorMessage;
                return RedirectToAction("List", "Announcement");
            }
            finally
            {
               client.Dispose();
            }
            TempData["notice"] = @"Вы успешно зарегистрировались на сайте!<br />" +
                                 "На указанный вами Email было отправлено письмо со ссылкой " +
                                 "для активации аккаунта";

            return RedirectToAction("List", "Announcement");
        }

        public ActionResult Activation(string token)
        {
            bool activated = WebSecurity.ConfirmAccount(token);
            if(activated)
            {
                TempData["notice"] = "Аккаунт успешно активирован";
                return RedirectToAction("List", "Announcement");
            }
            else
            {
                TempData["notice"] = ProjectConfiguration.Get.DataErrorMessage;
                return RedirectToAction("List", "Announcement");
            }
        }
    }
}
