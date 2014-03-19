namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddotnetmigration : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.NewSectionInCatalog", "SectionId", "dbo.CatalogSecondCategory", "Id", cascadeDelete: true);
            CreateIndex("dbo.NewSectionInCatalog", "SectionId", unique:true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.NewSectionInCatalog", new[] { "SectionId" });
            DropForeignKey("dbo.NewSectionInCatalog", "SectionId", "dbo.CatalogSecondCategory");
        }
    }
}
