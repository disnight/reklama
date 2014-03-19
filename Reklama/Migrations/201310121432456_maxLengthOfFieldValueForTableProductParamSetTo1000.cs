namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maxLengthOfFieldValueForTableProductParamSetTo1000 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductParamValue", "Value", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductParamValue", "Value", c => c.String(nullable: false, maxLength: 64));
        }
    }
}
