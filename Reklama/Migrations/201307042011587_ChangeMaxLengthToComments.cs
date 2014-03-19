namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMaxLengthToComments : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnnouncementComments", "Comment", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AnnouncementComments", "Comment", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
