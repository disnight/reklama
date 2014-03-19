using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain;

namespace Reklama.Models.ViewModels.Shared
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле `Email` обязательно для заполнения")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Неверно введен Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле `Пароль` обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Поле {0} должно быть не менее {2} символов длиной", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повтор пароля")]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}