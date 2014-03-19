using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Reklama.Models
{
    public class MailRobot: IDisposable
    {
        private SmtpClient _client = new SmtpClient("localhost");
        private MailMessage _message = new MailMessage();
        private bool _isDisposed;


        public MailRobot()
        {
            _client.Credentials = new System.Net.NetworkCredential(ProjectConfiguration.Get.LoginEmail, ProjectConfiguration.Get.PasswordEmail);
            _client.Port = ProjectConfiguration.Get.PortEmail;
            _client.Host = ProjectConfiguration.Get.HostEmail;
            _client.EnableSsl = true;
            _message.Priority = MailPriority.Normal;
            _message.SubjectEncoding = UTF8Encoding.UTF8;
            _message.BodyEncoding = UTF8Encoding.UTF8;
            _message.IsBodyHtml = false;
            _message.From = new MailAddress(ProjectConfiguration.Get.Email);
        }

        public void SetHeaderParams(bool isHtml, string from, MailPriority priority, bool isSsl)
        {
            _message.IsBodyHtml = isHtml;
            _message.From = new MailAddress(from);
            _message.Priority = priority;
            _client.EnableSsl = isSsl;
        }

        public void SendMessage(string recepient, string subject, string body)
        {
            _message.To.Add(recepient);
            _message.Subject = subject;
            _message.Body = body;

            _client.Send(_message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _message.Dispose();
                    _client.Dispose();
                }
            }

            _isDisposed = true;
        }
    }
}