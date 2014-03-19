namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfildinshop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shop", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shop", "IsActive");
        }
    }
}
