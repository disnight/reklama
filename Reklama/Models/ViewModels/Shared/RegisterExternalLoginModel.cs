using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reklama.Models.ViewModels.Shared
{
    public class RegisterExternalLoginModel
    {
        [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Неверно введен Email")]
        public string Email { get; set; }

        public string ExternalLoginData { get; set; }
    }
}