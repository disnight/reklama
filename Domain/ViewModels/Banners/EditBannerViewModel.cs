using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Domain.ViewModels.Banners
{
    public class EditBannerViewModel
    {
        public int ID { get; set; }
        public string Comments { get; set; }
        public bool IsShow { get; set; }
        public string Link { get; set; }
        public string ImagePath { get; set; }
        public string ImageDesc { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}