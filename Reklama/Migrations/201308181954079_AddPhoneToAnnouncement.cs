namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneToAnnouncement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcement", "Phone", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Announcement", "Phone");
        }
    }
}
