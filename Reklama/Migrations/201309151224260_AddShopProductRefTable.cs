namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShopProductRefTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopProductRef",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShopId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.ShopId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ShopId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ShopProductRef", new[] { "ProductId" });
            DropIndex("dbo.ShopProductRef", new[] { "ShopId" });
            DropForeignKey("dbo.ShopProductRef", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ShopProductRef", "ShopId", "dbo.Shop");
            DropTable("dbo.ShopProductRef");
        }
    }
}
