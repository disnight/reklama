namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnitToProductParamValues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductParamValue", "UnitId", c => c.Int());
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductParamValue", new[] { "UnitId" });
            DropForeignKey("dbo.ProductParamValue", "UnitId", "dbo.Unit");
            DropColumn("dbo.ProductParamValue", "UnitId");
        }
    }
}
