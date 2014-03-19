namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Computer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Computer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ComputerAnnouncementRef",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnouncementsId = c.Int(nullable: false),
                        ComputerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Computer", t => t.ComputerId, cascadeDelete: true)
                .ForeignKey("dbo.Announcement", t => t.AnnouncementsId, cascadeDelete: true)
                .Index(t => t.ComputerId)
                .Index(t => t.AnnouncementsId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ComputerAnnouncementRef", new[] { "AnnouncementsId" });
            DropIndex("dbo.ComputerAnnouncementRef", new[] { "ComputerId" });
            DropForeignKey("dbo.ComputerAnnouncementRef", "AnnouncementsId", "dbo.Announcement");
            DropForeignKey("dbo.ComputerAnnouncementRef", "ComputerId", "dbo.Computer");
            DropTable("dbo.ComputerAnnouncementRef");
            DropTable("dbo.Computer");
        }
    }
}
