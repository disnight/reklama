namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRealtyAndAdminPanel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        ExpiresAt = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LinkFlash = c.String(),
                        LinkImage = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActualArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.ArticleAndReviewBlocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BannerSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BannerId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GraphicBannerWithTexts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Image = c.String(),
                        Link = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewInCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubsectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subsection", t => t.SubsectionId, cascadeDelete: true)
                .Index(t => t.SubsectionId);
            
            CreateTable(
                "dbo.NewSectionInCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PopularProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PopularSectionInCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PopularSubsections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PremiumSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PremiumId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TopForumTopics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Realties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        ExpiredAt = c.DateTime(nullable: false),
                        UpTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsAuction = c.Boolean(nullable: false),
                        IsAgency = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        WithExtension = c.Boolean(nullable: false),
                        WithGarage = c.Boolean(nullable: false),
                        WithBasement = c.Boolean(nullable: false),
                        WithGarden = c.Boolean(nullable: false),
                        ForDays = c.Boolean(nullable: false),
                        CeillingHeight = c.Double(),
                        Floor = c.Int(),
                        FloorCount = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        RoomsCount = c.Byte(),
                        Square = c.Double(),
                        Name = c.String(nullable: false, maxLength: 180),
                        SmallDescription = c.String(nullable: false, maxLength: 600),
                        Description = c.String(nullable: false, maxLength: 1800),
                        Street = c.String(maxLength: 128),
                        AgencyName = c.String(maxLength: 64),
                        CategoryId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RealtyCategories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.RealtySections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Currency", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SectionId)
                .Index(t => t.CityId)
                .Index(t => t.CurrencyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RealtyCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RealtySections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RealtyPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Link = c.String(),
                        RealtyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Realties", t => t.RealtyId, cascadeDelete: true)
                .Index(t => t.RealtyId);
            
            CreateTable(
                "dbo.RealtyBookmarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RealtyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Realties", t => t.RealtyId, cascadeDelete: true)
                .Index(t => t.RealtyId);
            
            CreateTable(
                "dbo.RealtyFeedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        RealtyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Realties", t => t.RealtyId, cascadeDelete: true)
                .Index(t => t.RealtyId);
            
            CreateTable(
                "dbo.RealtyPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cost = c.Double(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        RealtyId = c.Int(nullable: false),
                        TransactionId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Realties", t => t.RealtyId, cascadeDelete: true)
                .Index(t => t.RealtyId);
            
            CreateTable(
                "dbo.RealtyPaymentHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplectedAt = c.DateTime(nullable: false),
                        Cost = c.Double(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ErrorMessage = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        PaysystemTransactionId = c.String(),
                        RealtyId = c.Int(nullable: false),
                        TransactionId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Realties", t => t.RealtyId, cascadeDelete: true)
                .Index(t => t.RealtyId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RealtyPaymentHistories", new[] { "RealtyId" });
            DropIndex("dbo.RealtyPayments", new[] { "RealtyId" });
            DropIndex("dbo.RealtyFeedbacks", new[] { "RealtyId" });
            DropIndex("dbo.RealtyBookmarks", new[] { "RealtyId" });
            DropIndex("dbo.RealtyPhotoes", new[] { "RealtyId" });
            DropIndex("dbo.Realties", new[] { "UserId" });
            DropIndex("dbo.Realties", new[] { "CurrencyId" });
            DropIndex("dbo.Realties", new[] { "CityId" });
            DropIndex("dbo.Realties", new[] { "SectionId" });
            DropIndex("dbo.Realties", new[] { "CategoryId" });
            DropIndex("dbo.NewInCatalogs", new[] { "SubsectionId" });
            DropIndex("dbo.ActualArticles", new[] { "ArticleId" });
            DropForeignKey("dbo.RealtyPaymentHistories", "RealtyId", "dbo.Realties");
            DropForeignKey("dbo.RealtyPayments", "RealtyId", "dbo.Realties");
            DropForeignKey("dbo.RealtyFeedbacks", "RealtyId", "dbo.Realties");
            DropForeignKey("dbo.RealtyBookmarks", "RealtyId", "dbo.Realties");
            DropForeignKey("dbo.RealtyPhotoes", "RealtyId", "dbo.Realties");
            DropForeignKey("dbo.Realties", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Realties", "CurrencyId", "dbo.Currency");
            DropForeignKey("dbo.Realties", "CityId", "dbo.City");
            DropForeignKey("dbo.Realties", "SectionId", "dbo.RealtySections");
            DropForeignKey("dbo.Realties", "CategoryId", "dbo.RealtyCategories");
            DropForeignKey("dbo.NewInCatalogs", "SubsectionId", "dbo.Subsection");
            DropForeignKey("dbo.ActualArticles", "ArticleId", "dbo.Articles");
            DropTable("dbo.RealtyPaymentHistories");
            DropTable("dbo.RealtyPayments");
            DropTable("dbo.RealtyFeedbacks");
            DropTable("dbo.RealtyBookmarks");
            DropTable("dbo.RealtyPhotoes");
            DropTable("dbo.RealtySections");
            DropTable("dbo.RealtyCategories");
            DropTable("dbo.Realties");
            DropTable("dbo.TopForumTopics");
            DropTable("dbo.PremiumSections");
            DropTable("dbo.PopularSubsections");
            DropTable("dbo.PopularSectionInCatalogs");
            DropTable("dbo.PopularProducts");
            DropTable("dbo.NewSectionInCatalogs");
            DropTable("dbo.NewInCatalogs");
            DropTable("dbo.GraphicBannerWithTexts");
            DropTable("dbo.BannerSections");
            DropTable("dbo.ArticleAndReviewBlocks");
            DropTable("dbo.ActualArticles");
            DropTable("dbo.Banners");
        }
    }
}
