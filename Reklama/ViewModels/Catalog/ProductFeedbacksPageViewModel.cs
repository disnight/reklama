using PagedList;

namespace Reklama.ViewModels.Catalog
{
    public class ProductFeedbacksPageViewModel
    {
        public IPagedList<FeedbackViewModel> Comments { get; set; }

        public Data.Entities.Product Product { get; set; }
    }
}