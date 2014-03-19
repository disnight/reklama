namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnouncementConfigurationAndIncreasedSearchProviderDescription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnnouncementConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slogan = c.String(maxLength: 255),
                        Premium1Id = c.Int(nullable: false),
                        Premium2Id = c.Int(nullable: false),
                        Premium3Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Premium", t => t.Premium1Id)
                .ForeignKey("dbo.Premium", t => t.Premium2Id)
                .ForeignKey("dbo.Premium", t => t.Premium3Id)
                .Index(t => t.Premium1Id)
                .Index(t => t.Premium2Id)
                .Index(t => t.Premium3Id);
            
            AlterColumn("dbo.Announcement", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Articles", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Realty", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropIndex("dbo.AnnouncementConfig", new[] { "Premium3Id" });
            DropIndex("dbo.AnnouncementConfig", new[] { "Premium2Id" });
            DropIndex("dbo.AnnouncementConfig", new[] { "Premium1Id" });
            DropForeignKey("dbo.AnnouncementConfig", "Premium3Id", "dbo.Premium");
            DropForeignKey("dbo.AnnouncementConfig", "Premium2Id", "dbo.Premium");
            DropForeignKey("dbo.AnnouncementConfig", "Premium1Id", "dbo.Premium");
            AlterColumn("dbo.Realty", "Description", c => c.String(nullable: false, maxLength: 1800));
            AlterColumn("dbo.Articles", "Description", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Announcement", "Description", c => c.String(nullable: false, maxLength: 4000));
            DropTable("dbo.AnnouncementConfig");
        }
    }
}
