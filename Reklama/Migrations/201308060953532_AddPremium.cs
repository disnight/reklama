namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPremium : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Premium",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        Description = c.String(nullable: false, maxLength: 255),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lifetime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.PremiumSection");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PremiumSection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PremiumId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Premium");
        }
    }
}
