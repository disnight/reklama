namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTypeRealtyPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Realty", "Price", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Realty", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
