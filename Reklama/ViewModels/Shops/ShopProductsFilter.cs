using Domain.Enums;

namespace Reklama.ViewModels.Shops
{
    public class ShopProductsFilter
    {
        public int CategoryID { get; set; }
        public int ShopID { get; set; }
        public SortOrderParams SortOrder { get; set; }
        public SortOptionsParams SortOptions { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public ShopProductsFilter()
        {
            Page = 1;
            PageSize = ProjectConfiguration.Get.ItemsOnPage[0];
            SortOrder = SortOrderParams.Ascending;
            SortOptions = SortOptionsParams.ByName;
        } 
    }
}