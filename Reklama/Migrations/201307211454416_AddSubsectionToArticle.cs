namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubsectionToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "SubsectionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "SubsectionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Articles", new[] { "SubsectionId" });
            DropColumn("dbo.Articles", "SubsectionId");
        }
    }
}
