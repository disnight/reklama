namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnouncementsPremiumsRef : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnnouncementsPremiumsRef",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnouncementId = c.Int(nullable: false),
                        AnnouncementSectionId = c.Int(nullable: false),
                        PremiumId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ExpiresAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Announcement", t => t.AnnouncementId, cascadeDelete: true)
                .ForeignKey("dbo.Section", t => t.AnnouncementSectionId)
                .ForeignKey("dbo.Premium", t => t.PremiumId, cascadeDelete: true)
                .Index(t => t.AnnouncementId)
                .Index(t => t.AnnouncementSectionId)
                .Index(t => t.PremiumId);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AnnouncementsPremiumsRef", new[] { "PremiumId" });
            DropIndex("dbo.AnnouncementsPremiumsRef", new[] { "AnnouncementSectionId" });
            DropIndex("dbo.AnnouncementsPremiumsRef", new[] { "AnnouncementId" });
            DropForeignKey("dbo.AnnouncementsPremiumsRef", "PremiumId", "dbo.Premium");
            DropForeignKey("dbo.AnnouncementsPremiumsRef", "AnnouncementSectionId", "dbo.Section");
            DropForeignKey("dbo.AnnouncementsPremiumsRef", "AnnouncementId", "dbo.Announcement");
            DropTable("dbo.AnnouncementsPremiumsRef");
        }
    }
}
