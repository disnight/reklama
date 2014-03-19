namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addxzmigration : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.PopularSectionInCatalog", "SectionId", "dbo.CatalogSecondCategory", "Id", cascadeDelete: true);
            CreateIndex("dbo.PopularSectionInCatalog", "SectionId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PopularSectionInCatalog", new[] { "SectionId" });
            DropForeignKey("dbo.PopularSectionInCatalog", "SectionId", "dbo.CatalogSecondCategory");
        }
    }
}
