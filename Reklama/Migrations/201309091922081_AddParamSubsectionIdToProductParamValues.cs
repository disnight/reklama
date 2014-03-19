namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParamSubsectionIdToProductParamValues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductParamValue", "ParamSubsectionId", c => c.Int());
            //AddForeignKey("dbo.ProductParamValue", "ParamSubsectionId", "dbo.CatalogParamSubsection", "Id");
            CreateIndex("dbo.ProductParamValue", "ParamSubsectionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductParamValue", "SecondCategoryId", c => c.Int());
            DropIndex("dbo.ProductParamValue", new[] { "ParamSubsectionId" });
            DropForeignKey("dbo.ProductParamValue", "ParamSubsectionId", "dbo.CatalogParamSubsection");
            DropColumn("dbo.ProductParamValue", "ParamSubsectionId");
            CreateIndex("dbo.ProductParamValue", "SecondCategoryId");
            AddForeignKey("dbo.ProductParamValue", "SecondCategoryId", "dbo.CatalogSecondCategory", "Id");
        }
    }
}
