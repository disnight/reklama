namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSmallDescriptionToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "SmallDescription", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "SmallDescription");
        }
    }
}
