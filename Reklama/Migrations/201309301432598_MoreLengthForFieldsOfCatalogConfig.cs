namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreLengthForFieldsOfCatalogConfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CatalogConfigTable", "Slogan", c => c.String(maxLength: 1000));
            AlterColumn("dbo.CatalogConfigTable", "WarningText", c => c.String(maxLength: 1000));
            AlterColumn("dbo.CatalogConfigTable", "RegShopPromoText", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CatalogConfigTable", "RegShopPromoText", c => c.String(maxLength: 255));
            AlterColumn("dbo.CatalogConfigTable", "WarningText", c => c.String(maxLength: 255));
            AlterColumn("dbo.CatalogConfigTable", "Slogan", c => c.String(maxLength: 255));
        }
    }
}
