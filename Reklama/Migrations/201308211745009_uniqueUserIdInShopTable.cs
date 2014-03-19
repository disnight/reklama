namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueUserIdInShopTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Shop");
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
                            Phone = c.String(nullable: false, maxLength: 255),
                            FullName = c.String(nullable: false, maxLength: 128),
                            CompanyName = c.String(nullable: false, maxLength: 128),
                            Description = c.String(maxLength: 1000),
                            IsActive = c.Boolean(nullable: false),
                            Monday = c.Boolean(nullable: false),
                            Tuesday = c.Boolean(nullable: false),
                            Wednesday = c.Boolean(nullable: false),
                            Thursday = c.Boolean(nullable: false),
                            Friday = c.Boolean(nullable: false),
                            Saturday = c.Boolean(nullable: false),
                            Sunday = c.Boolean(nullable: false),
                            Requisites = c.String(nullable: false, maxLength: 1000)
                        })
                        .PrimaryKey(t => t.Id)
                        .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                        .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                        .Index(t => t.UserId, unique: true)
                        .Index(t => t.CityId);
        }
        
        public override void Down()
        {
        }
    }
}
