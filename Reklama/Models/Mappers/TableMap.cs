using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Mappers
{
    public static class TableMap
    {
        public static readonly string UserProfileTable = "UserProfile";
        public static readonly string SectionTable = "Section";
        public static readonly string SubsectionTable = "Subsection";
        public static readonly string CategoryTable = "Category";
        public static readonly string AnnouncementTable = "Announcement";
        public static readonly string AnnouncementCommentTable = "AnnouncementComment";
        public static readonly string AnnouncementsPremiumsRefTable = "AnnouncementsPremiumsRef";


        //Admin panel
        public static readonly string BannerTable = "Banner";
        public static readonly string ActualArticleTable = "ActualArticle";
        public static readonly string ArticleAndReviewBlockTable = "ArticleAndReviewBlock";
        public static readonly string BannerSectionTable = "BannerSection";
        public static readonly string ConfigTable = "Config";
        public static readonly string GraphicBannerWithTextTable = "GraphicBannerWithText";
        public static readonly string NewInCatalogTable = "NewInCatalog";
        public static readonly string NewSectionInCatalogTable = "NewSectionInCatalog";
        public static readonly string PopularProductTable = "PopularProduct";
        public static readonly string PopularSectionInCatalogTable = "PopularSectionInCatalog";
        public static readonly string PopularSubsectionTable = "PopularSubsection";
        public static readonly string PremiumTable = "Premium";
        public static readonly string TopForumTopicTable = "TopForumTopic";
        public static readonly string AnnouncementConfigTable = "AnnouncementConfig";
        public static readonly string ArticleConfigTable = "ArticleConfig";
        public static readonly string PaymentTable = "Payment";
        public static readonly string PopularAnnoucementTable = "PopularAnnoucement";


        //Realty
        public static readonly string RealtyTable = "Realty";
        public static readonly string RealtyBookmarkTable = "RealtyBookmark";
        public static readonly string RealtyCategoryTable = "RealtyCategory";
        public static readonly string RealtyCommentTable = "RealtyComment";
        public static readonly string RealtyPaymentTable = "RealtyPayment";
        public static readonly string RealtyPaymentHistoryTable = "RealtyPaymentHistory";
        public static readonly string RealtyPhotoTable = "RealtyPhoto";
        public static readonly string RealtySectionTable = "RealtySection";

        //Shared
        public static readonly string CityTable = "City";
        public static readonly string CurrencyTable = "Currency";

        // Other
        public static readonly string MenuTable = "Menu";
        public static readonly string PageTable = "Page";
        public static readonly string MenuPageRefTable = "MenuPageRef";

        public static readonly string ComputerTable = "Computer";
        public static readonly string ComputerAnnouncementRefTable = "ComputerAnnouncementRef";
        public static readonly string ComputerRealtyRefTable = "ComputerRealtyRef";

        // Article
        public static readonly string ArticleCommentTable = "ArticleComment";
        public static readonly string ArticleSectionTable = "ArticleSection";
        public static readonly string ArticleSubsectionTable = "ArticleSubsection";

        //Catalog
        public static readonly string CatalogConfigTable = "CatalogConfigTable";
        public static readonly string ShopTable = "Shop";
        public static readonly string ShopCommentTable = "ShopComment";
        public static readonly string CatalogCategoryTable = "CatalogCategory";
        public static readonly string CatalogSecondCategoryTable = "CatalogSecondCategory";
        public static readonly string CatalogThirdCategoryTable = "CatalogThirdCategory";
        public static readonly string CatalogParamSectionTable = "CatalogParamSection";
        public static readonly string CatalogParamSubsectionTable = "CatalogParamSubsection";
        public static readonly string ProductTable = "Product";
        public static readonly string ProductFeedbackTable = "ProductFeedback";
        public static readonly string ProductBookmarkTable = "ProductBookmark";
        public static readonly string ProductImageTable = "ProductImage";
        public static readonly string ProductParamTable = "ProductParam";
        public static readonly string ProductParamValueTable = "ProductParamValue";
        public static readonly string ProductSectionParamRefTable = "ProductSectionParamRef";
        public static readonly string ProductSubsectionParamRefTable = "ProductSubsectionParamRef";
        public static readonly string UnitTable = "Unit";
        public static readonly string ShopCategoryTable = "ShopCategoryRef";
        public static readonly string ShopProductTable = "ShopProductRef";
    }
}