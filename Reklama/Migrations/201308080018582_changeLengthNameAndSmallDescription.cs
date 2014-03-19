namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeLengthNameAndSmallDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcement", "Name", c => c.String(nullable: false, maxLength: 180));
            AlterColumn("dbo.Announcement", "SmallDescription", c => c.String(nullable: false, maxLength: 600));
            AlterColumn("dbo.Articles", "Name", c => c.String(nullable: false, maxLength: 180));
            AlterColumn("dbo.Articles", "SmallDescription", c => c.String(nullable: false, maxLength: 600));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "SmallDescription", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Articles", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Announcement", "SmallDescription", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Announcement", "Name", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
