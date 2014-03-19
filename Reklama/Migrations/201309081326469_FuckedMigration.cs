namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FuckedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 1000),
                        CreatedAt = c.DateTime(nullable: false),
                        ShopId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.ShopId)
                .ForeignKey("dbo.UserProfile", t => t.UserId)
                .Index(t => t.ShopId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CatalogCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CatalogParamSubsection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        SecondCategoryId = c.Int(nullable: false),
                        Slogan = c.String(nullable: false, maxLength: 255),
                        CatalogCategory_Id = c.Int(),
                        CatalogParamSection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogSecondCategory", t => t.SecondCategoryId)
                .ForeignKey("dbo.CatalogCategory", t => t.CatalogCategory_Id)
                .ForeignKey("dbo.CatalogParamSection", t => t.CatalogParamSection_Id)
                .Index(t => t.SecondCategoryId)
                .Index(t => t.CatalogCategory_Id)
                .Index(t => t.CatalogParamSection_Id);
            
            CreateTable(
                "dbo.CatalogSecondCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        CategoryId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogCategory", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductSubsectionParamRef",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParamId = c.Int(nullable: false),
                        SubsectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductParam", t => t.ParamId)
                .ForeignKey("dbo.CatalogParamSubsection", t => t.SubsectionId)
                .Index(t => t.ParamId)
                .Index(t => t.SubsectionId);
            
            CreateTable(
                "dbo.ProductParam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CatalogThirdCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        SecondCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogSecondCategory", t => t.SecondCategoryId)
                .Index(t => t.SecondCategoryId);
            
            CreateTable(
                "dbo.CatalogParamSection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductSectionParamRef",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParamId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CatalogParamSection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductParam", t => t.ParamId)
                .ForeignKey("dbo.CatalogCategory", t => t.CategoryId)
                .ForeignKey("dbo.CatalogParamSection", t => t.CatalogParamSection_Id)
                .Index(t => t.ParamId)
                .Index(t => t.CategoryId)
                .Index(t => t.CatalogParamSection_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Image = c.String(nullable: false),
                        SecondCategoryId = c.Int(nullable: false),
                        ThirdCategoryId = c.Int(nullable: false),
                        ReviewLink = c.String(maxLength: 255),
                        Name = c.String(nullable: false, maxLength: 180),
                        SmallDescription = c.String(nullable: false, maxLength: 600),
                        Description = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogCategory", t => t.CategoryId)
                .ForeignKey("dbo.CatalogSecondCategory", t => t.SecondCategoryId)
                .ForeignKey("dbo.CatalogThirdCategory", t => t.ThirdCategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.SecondCategoryId)
                .Index(t => t.ThirdCategoryId);
            
            CreateTable(
                "dbo.ProductImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Link = c.String(nullable: false, maxLength: 255),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductFeedback",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 1000),
                        CreatedAt = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.UserProfile", t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProductParamValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ParamId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.ProductParam", t => t.ParamId)
                .Index(t => t.ProductId)
                .Index(t => t.ParamId);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShopCategoryRef",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShopId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.ShopId)
                .ForeignKey("dbo.CatalogSecondCategory", t => t.CategoryId)
                .Index(t => t.ShopId)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Shop", "Icq", c => c.String(maxLength: 10));
            AddColumn("dbo.Shop", "Skype", c => c.String(maxLength: 32));
            AlterColumn("dbo.RealtyComment", "Comment", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            DropIndex("dbo.ShopCategoryRef", new[] { "CategoryId" });
            DropIndex("dbo.ShopCategoryRef", new[] { "ShopId" });
            DropIndex("dbo.ProductParamValue", new[] { "ParamId" });
            DropIndex("dbo.ProductParamValue", new[] { "ProductId" });
            DropIndex("dbo.ProductFeedback", new[] { "UserId" });
            DropIndex("dbo.ProductFeedback", new[] { "ProductId" });
            DropIndex("dbo.ProductImage", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "ThirdCategoryId" });
            DropIndex("dbo.Product", new[] { "SecondCategoryId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.ProductSectionParamRef", new[] { "CatalogParamSection_Id" });
            DropIndex("dbo.ProductSectionParamRef", new[] { "CategoryId" });
            DropIndex("dbo.ProductSectionParamRef", new[] { "ParamId" });
            DropIndex("dbo.CatalogThirdCategory", new[] { "SecondCategoryId" });
            DropIndex("dbo.ProductSubsectionParamRef", new[] { "SubsectionId" });
            DropIndex("dbo.ProductSubsectionParamRef", new[] { "ParamId" });
            DropIndex("dbo.CatalogSecondCategory", new[] { "CategoryId" });
            DropIndex("dbo.CatalogParamSubsection", new[] { "CatalogParamSection_Id" });
            DropIndex("dbo.CatalogParamSubsection", new[] { "CatalogCategory_Id" });
            DropIndex("dbo.CatalogParamSubsection", new[] { "SecondCategoryId" });
            DropIndex("dbo.ShopComment", new[] { "UserId" });
            DropIndex("dbo.ShopComment", new[] { "ShopId" });
            DropForeignKey("dbo.ShopCategoryRef", "CategoryId", "dbo.CatalogSecondCategory");
            DropForeignKey("dbo.ShopCategoryRef", "ShopId", "dbo.Shop");
            DropForeignKey("dbo.ProductParamValue", "ParamId", "dbo.ProductParam");
            DropForeignKey("dbo.ProductParamValue", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductFeedback", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.ProductFeedback", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductImage", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "ThirdCategoryId", "dbo.CatalogThirdCategory");
            DropForeignKey("dbo.Product", "SecondCategoryId", "dbo.CatalogSecondCategory");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.CatalogCategory");
            DropForeignKey("dbo.ProductSectionParamRef", "CatalogParamSection_Id", "dbo.CatalogParamSection");
            DropForeignKey("dbo.ProductSectionParamRef", "CategoryId", "dbo.CatalogCategory");
            DropForeignKey("dbo.ProductSectionParamRef", "ParamId", "dbo.ProductParam");
            DropForeignKey("dbo.CatalogThirdCategory", "SecondCategoryId", "dbo.CatalogSecondCategory");
            DropForeignKey("dbo.ProductSubsectionParamRef", "SubsectionId", "dbo.CatalogParamSubsection");
            DropForeignKey("dbo.ProductSubsectionParamRef", "ParamId", "dbo.ProductParam");
            DropForeignKey("dbo.CatalogSecondCategory", "CategoryId", "dbo.CatalogCategory");
            DropForeignKey("dbo.CatalogParamSubsection", "CatalogParamSection_Id", "dbo.CatalogParamSection");
            DropForeignKey("dbo.CatalogParamSubsection", "CatalogCategory_Id", "dbo.CatalogCategory");
            DropForeignKey("dbo.CatalogParamSubsection", "SecondCategoryId", "dbo.CatalogSecondCategory");
            DropForeignKey("dbo.ShopComment", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.ShopComment", "ShopId", "dbo.Shop");
            AlterColumn("dbo.RealtyComment", "Comment", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.Shop", "Skype");
            DropColumn("dbo.Shop", "Icq");
            DropTable("dbo.ShopCategoryRef");
            DropTable("dbo.Unit");
            DropTable("dbo.ProductParamValue");
            DropTable("dbo.ProductFeedback");
            DropTable("dbo.ProductImage");
            DropTable("dbo.Product");
            DropTable("dbo.ProductSectionParamRef");
            DropTable("dbo.CatalogParamSection");
            DropTable("dbo.CatalogThirdCategory");
            DropTable("dbo.ProductParam");
            DropTable("dbo.ProductSubsectionParamRef");
            DropTable("dbo.CatalogSecondCategory");
            DropTable("dbo.CatalogParamSubsection");
            DropTable("dbo.CatalogCategory");
            DropTable("dbo.ShopComment");
        }
    }
}
