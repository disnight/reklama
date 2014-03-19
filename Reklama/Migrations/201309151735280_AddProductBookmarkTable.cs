namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductBookmarkTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductBookmark",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.UserProfile", t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductBookmark", new[] { "UserId" });
            DropIndex("dbo.ProductBookmark", new[] { "ProductId" });
            DropForeignKey("dbo.ProductBookmark", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.ProductBookmark", "ProductId", "dbo.Product");
            DropTable("dbo.ProductBookmark");
        }
    }
}
