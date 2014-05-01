namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ContactName_And_ComtactEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcement", "ContactName", c => c.String());
            AddColumn("dbo.Announcement", "ContactEmail", c => c.String());
            AddColumn("dbo.Realty", "ContactName", c => c.String());
            AddColumn("dbo.Realty", "ContactEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Realty", "ContactEmail");
            DropColumn("dbo.Realty", "ContactName");
            DropColumn("dbo.Announcement", "ContactEmail");
            DropColumn("dbo.Announcement", "ContactName");
        }
    }
}
