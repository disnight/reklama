namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nonRequiredCategoriesInProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "SecondCategoryId", "dbo.CatalogSecondCategory");
            DropForeignKey("dbo.Product", "ThirdCategoryId", "dbo.CatalogThirdCategory");
            DropIndex("dbo.Product", new[] { "SecondCategoryId" });
            DropIndex("dbo.Product", new[] { "ThirdCategoryId" });
            AlterColumn("dbo.Product", "SecondCategoryId", c => c.Int());
            AlterColumn("dbo.Product", "ThirdCategoryId", c => c.Int());
            AddForeignKey("dbo.Product", "SecondCategoryId", "dbo.CatalogSecondCategory", "Id");
            AddForeignKey("dbo.Product", "ThirdCategoryId", "dbo.CatalogThirdCategory", "Id");
            CreateIndex("dbo.Product", "SecondCategoryId");
            CreateIndex("dbo.Product", "ThirdCategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Product", new[] { "ThirdCategoryId" });
            DropIndex("dbo.Product", new[] { "SecondCategoryId" });
            DropForeignKey("dbo.Product", "ThirdCategoryId", "dbo.CatalogThirdCategory");
            DropForeignKey("dbo.Product", "SecondCategoryId", "dbo.CatalogSecondCategory");
            AlterColumn("dbo.Product", "ThirdCategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Product", "SecondCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "ThirdCategoryId");
            CreateIndex("dbo.Product", "SecondCategoryId");
            AddForeignKey("dbo.Product", "ThirdCategoryId", "dbo.CatalogThirdCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Product", "SecondCategoryId", "dbo.CatalogSecondCategory", "Id", cascadeDelete: true);
        }
    }
}
