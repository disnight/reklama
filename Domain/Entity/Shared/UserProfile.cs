using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Entity.Announcements;

namespace Domain.Entity.Shared
{
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Display(Name = "Имя")]
        [StringLength(32, ErrorMessage = "Максимальная длина поля - 32 символа")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(32, ErrorMessage = "Максимальная длина поля - 32 символа")]
        public string Surname { get; set; }

        [StringLength(128, ErrorMessage = "Минимальная длина поля - 6 символов, максимальная - 128", MinimumLength = 6)]
        public string Email { get; set; }

        [StringLength(32, ErrorMessage = "Максимальная длина поля - 32 символа")]
        public string Skype { get; set; }

        [StringLength(32, ErrorMessage = "Максимальная длина поля - 32 символа")]
        public string Icq { get; set; }

        [Display(Name = "Телефон")]
        [StringLength(32, ErrorMessage = "Максимальная длина поля - 32 символа")]
        public string Phone { get; set; }

        [StringLength(255, ErrorMessage = "Максимальная длина поля - 255 символа")]
        public string AvatarLink { get; set; }

        [Display(Name = "Адрес сайта")]
        [StringLength(128, ErrorMessage = "Максимальная длина поля - 128 символа")]
        public string Site { get; set; }

        [Display(Name = "О себе кратко")]
        [StringLength(255, ErrorMessage = "Максимальная длина поля - 255 символа")]
        public string About { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date, ErrorMessage = "Неверно указана дата")]
        public DateTime? Birthday { get; set; }


        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<AnnouncementBookmark> AnnouncementBookmarks { get; set; }
        public virtual ICollection<PrivateMessage> PrivateMessages { get; set; }



        public override string ToString()
        {
            return (Surname != null || Name != null) 
                ? string.Format("{0} {1}", Name, Surname) 
                : Email;
        }


        public string FullName()
        {
            string fullName = string.Format("{0} {1}", Name, Surname);
            if (Surname != null || Name != null)
            {
                fullName += "<br />";
            }
            fullName += Email;
            return fullName;
        }
    }
}