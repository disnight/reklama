namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FuckedMigration3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CatalogParamSubsection", "CatalogCategory_Id", "dbo.CatalogCategory");
            DropIndex("dbo.CatalogParamSubsection", new[] { "CatalogCategory_Id" });
            DropColumn("dbo.CatalogParamSubsection", "CatalogCategory_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CatalogParamSubsection", "CatalogCategory_Id", c => c.Int());
            CreateIndex("dbo.CatalogParamSubsection", "CatalogCategory_Id");
            AddForeignKey("dbo.CatalogParamSubsection", "CatalogCategory_Id", "dbo.CatalogCategory", "Id");
        }
    }
}
