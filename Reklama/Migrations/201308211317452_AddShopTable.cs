namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShopTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shop",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Debt = c.Double(nullable: false),
                        ImageLogo = c.String(maxLength: 255),
                        CityId = c.Int(nullable: false),
                        Site = c.String(maxLength: 255),
                        Name = c.String(nullable: false, maxLength: 64),
                        Phone = c.String(nullable: false, maxLength: 32),
                        FullName = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Shop", new[] { "CityId" });
            DropIndex("dbo.Shop", new[] { "UserId" });
            DropForeignKey("dbo.Shop", "CityId", "dbo.City");
            DropForeignKey("dbo.Shop", "UserId", "dbo.UserProfile");
            DropTable("dbo.Shop");
        }
    }
}
