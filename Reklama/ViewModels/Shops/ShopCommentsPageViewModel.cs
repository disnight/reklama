using PagedList;
using Reklama.ViewModels.Catalog;

namespace Reklama.ViewModels.Shops
{
    public class ShopCommentsPageViewModel
    {
        public IPagedList<FeedbackViewModel> Comments { get; set; }
        public int ShopID { get; set; }
    }
}