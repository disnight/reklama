using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entity.Shared
{
    public class PrivateMessage: BaseEntity
    {
        public int SenderId { get; set; }
        public int RecepientId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        [AllowHtml]
        [StringLength(128, ErrorMessage = "Максимальная длина - 128 символов")]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "Максимальная длина - 2000 символов")]
        [Display(Name = "Сообщение")]
        public string Message { get; set; }


        [ForeignKey("SenderId")]
        public virtual UserProfile Sender { get; set; }

        [ForeignKey("RecepientId")]
        public virtual UserProfile Recepient { get; set; }
    }
}
