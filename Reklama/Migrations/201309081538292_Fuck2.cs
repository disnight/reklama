namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fuck2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CatalogParamSubsection", "CatalogParamSection_Id", "dbo.CatalogParamSection");
            DropForeignKey("dbo.ProductSectionParamRef", "CatalogParamSection_Id", "dbo.CatalogParamSection");
            DropIndex("dbo.CatalogParamSubsection", new[] { "CatalogParamSection_Id" });
            DropIndex("dbo.ProductSectionParamRef", new[] { "CatalogParamSection_Id" });
            DropColumn("dbo.CatalogParamSubsection", "CatalogParamSection_Id");
            DropColumn("dbo.ProductSectionParamRef", "CatalogParamSection_Id");
            DropTable("dbo.CatalogParamSection");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CatalogParamSection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductSectionParamRef", "CatalogParamSection_Id", c => c.Int());
            AddColumn("dbo.CatalogParamSubsection", "CatalogParamSection_Id", c => c.Int());
            CreateIndex("dbo.ProductSectionParamRef", "CatalogParamSection_Id");
            CreateIndex("dbo.CatalogParamSubsection", "CatalogParamSection_Id");
            AddForeignKey("dbo.ProductSectionParamRef", "CatalogParamSection_Id", "dbo.CatalogParamSection", "Id");
            AddForeignKey("dbo.CatalogParamSubsection", "CatalogParamSection_Id", "dbo.CatalogParamSection", "Id");
        }
    }
}
