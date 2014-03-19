using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reklama.Models.ViewModels.Announcement
{
    public class AnnouncementCommentViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CommentId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int AnnouncementId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Минимальная длина поля - 3 символа, максимальная - 1000")]
        public string Comment { get; set; }
    }
}