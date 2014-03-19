namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addForeignKey : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PopularAnnouncements");
            CreateTable(
                "dbo.PopularAnnouncements",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AnnouncementId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Announcement", t => t.AnnouncementId)
                .Index(t => t.AnnouncementId);
        }
        
        public override void Down()
        {
            AddColumn("dbo.PopularAnnouncements", "AnnoucementId", c => c.Int(nullable: false));
            DropIndex("dbo.PopularAnnouncements", new[] { "AnnouncementId" });
            DropForeignKey("dbo.PopularAnnouncements", "AnnouncementId", "dbo.Announcement");
            DropColumn("dbo.PopularAnnouncements", "AnnouncementId");
        }
    }
}
