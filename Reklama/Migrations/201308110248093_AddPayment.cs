namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPayment : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AnnouncementsPremiumsRef", name: "AnnouncementId", newName: "AdId");
            RenameColumn(table: "dbo.AnnouncementsPremiumsRef", name: "AnnouncementSectionId", newName: "AdSectionId");
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaysystemInvId = c.Int(nullable: false),
                        AdId = c.Int(nullable: false),
                        SiteCategory = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        PremiumId = c.Int(),
                        SectionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payment");
            RenameColumn(table: "dbo.AnnouncementsPremiumsRef", name: "AdSectionId", newName: "AnnouncementSectionId");
            RenameColumn(table: "dbo.AnnouncementsPremiumsRef", name: "AdId", newName: "AnnouncementId");
        }
    }
}
