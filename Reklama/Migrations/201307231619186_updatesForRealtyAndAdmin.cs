namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesForRealtyAndAdmin : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Banners", newName: "Banner");
            RenameTable(name: "dbo.ActualArticles", newName: "ActualArticle");
            RenameTable(name: "dbo.ArticleAndReviewBlocks", newName: "ArticleAndReviewBlock");
            RenameTable(name: "dbo.BannerSections", newName: "BannerSection");
            RenameTable(name: "dbo.GraphicBannerWithTexts", newName: "GraphicBannerWithText");
            RenameTable(name: "dbo.NewInCatalogs", newName: "NewInCatalog");
            RenameTable(name: "dbo.NewSectionInCatalogs", newName: "NewSectionInCatalog");
            RenameTable(name: "dbo.PopularProducts", newName: "PopularProduct");
            RenameTable(name: "dbo.PopularSectionInCatalogs", newName: "PopularSectionInCatalog");
            RenameTable(name: "dbo.PopularSubsections", newName: "PopularSubsection");
            RenameTable(name: "dbo.PremiumSections", newName: "PremiumSection");
            RenameTable(name: "dbo.TopForumTopics", newName: "TopForumTopic");
            RenameTable(name: "dbo.Realties", newName: "Realty");
            RenameTable(name: "dbo.RealtyCategories", newName: "RealtyCategory");
            RenameTable(name: "dbo.RealtySections", newName: "RealtySection");
            RenameTable(name: "dbo.RealtyPhotoes", newName: "RealtyPhoto");
            RenameTable(name: "dbo.RealtyBookmarks", newName: "RealtyBookmark");
            RenameTable(name: "dbo.RealtyFeedbacks", newName: "RealtyFeedback");
            RenameTable(name: "dbo.RealtyPayments", newName: "RealtyPayment");
            RenameTable(name: "dbo.RealtyPaymentHistories", newName: "RealtyPaymentHistory");
            AlterColumn("dbo.Banner", "LinkFlash", c => c.String(maxLength: 255));
            AlterColumn("dbo.Banner", "LinkImage", c => c.String(maxLength: 255));
            AlterColumn("dbo.Banner", "Url", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.GraphicBannerWithText", "Image", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.GraphicBannerWithText", "Link", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.GraphicBannerWithText", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Realty", "ExpiredAt", c => c.DateTime());
            AlterColumn("dbo.Realty", "ForDays", c => c.Boolean());
            AlterColumn("dbo.RealtyCategory", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.RealtySection", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.RealtyPhoto", "Link", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.RealtyFeedback", "Comment", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.RealtyPaymentHistory", "ErrorMessage", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.RealtyPaymentHistory", "PaysystemTransactionId", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RealtyPaymentHistory", "PaysystemTransactionId", c => c.String());
            AlterColumn("dbo.RealtyPaymentHistory", "ErrorMessage", c => c.String());
            AlterColumn("dbo.RealtyFeedback", "Comment", c => c.String());
            AlterColumn("dbo.RealtyPhoto", "Link", c => c.String());
            AlterColumn("dbo.RealtySection", "Name", c => c.String());
            AlterColumn("dbo.RealtyCategory", "Name", c => c.String());
            AlterColumn("dbo.Realty", "ForDays", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Realty", "ExpiredAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.GraphicBannerWithText", "Name", c => c.String());
            AlterColumn("dbo.GraphicBannerWithText", "Link", c => c.String());
            AlterColumn("dbo.GraphicBannerWithText", "Image", c => c.String());
            AlterColumn("dbo.Banner", "Url", c => c.String());
            AlterColumn("dbo.Banner", "LinkImage", c => c.String());
            AlterColumn("dbo.Banner", "LinkFlash", c => c.String());
            RenameTable(name: "dbo.RealtyPaymentHistory", newName: "RealtyPaymentHistories");
            RenameTable(name: "dbo.RealtyPayment", newName: "RealtyPayments");
            RenameTable(name: "dbo.RealtyFeedback", newName: "RealtyFeedbacks");
            RenameTable(name: "dbo.RealtyBookmark", newName: "RealtyBookmarks");
            RenameTable(name: "dbo.RealtyPhoto", newName: "RealtyPhotoes");
            RenameTable(name: "dbo.RealtySection", newName: "RealtySections");
            RenameTable(name: "dbo.RealtyCategory", newName: "RealtyCategories");
            RenameTable(name: "dbo.Realty", newName: "Realties");
            RenameTable(name: "dbo.TopForumTopic", newName: "TopForumTopics");
            RenameTable(name: "dbo.PremiumSection", newName: "PremiumSections");
            RenameTable(name: "dbo.PopularSubsection", newName: "PopularSubsections");
            RenameTable(name: "dbo.PopularSectionInCatalog", newName: "PopularSectionInCatalogs");
            RenameTable(name: "dbo.PopularProduct", newName: "PopularProducts");
            RenameTable(name: "dbo.NewSectionInCatalog", newName: "NewSectionInCatalogs");
            RenameTable(name: "dbo.NewInCatalog", newName: "NewInCatalogs");
            RenameTable(name: "dbo.GraphicBannerWithText", newName: "GraphicBannerWithTexts");
            RenameTable(name: "dbo.BannerSection", newName: "BannerSections");
            RenameTable(name: "dbo.ArticleAndReviewBlock", newName: "ArticleAndReviewBlocks");
            RenameTable(name: "dbo.ActualArticle", newName: "ActualArticles");
            RenameTable(name: "dbo.Banner", newName: "Banners");
        }
    }
}
