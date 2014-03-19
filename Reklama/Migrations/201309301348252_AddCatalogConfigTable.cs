namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCatalogConfigTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CatalogConfigTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slogan = c.String(maxLength: 255),
                        PromoText = c.String(maxLength: 255),
                        WarningText = c.String(maxLength: 255),
                        RegShopPromoText = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CatalogConfigTable");
        }
    }
}
