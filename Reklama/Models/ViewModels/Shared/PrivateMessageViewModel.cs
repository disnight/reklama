using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reklama.Models.ViewModels.Shared
{
    public class PrivateMessageViewModel
    {
        [StringLength(128, ErrorMessage = "Максимальная длина - 128 символов")]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Необходимо ввести сообщение")]
        [StringLength(2000, ErrorMessage = "Максимальная длина - 2000 символов")]
        [Display(Name = "Сообщение")]
        public string Message { get; set; }

        [Display(Name = "Кому")]
        public string Recepient { get; set; }

        [Display(Name = "От")]
        public string Sender { get; set; }

        public int RecepientId { get; set; }
    }
}