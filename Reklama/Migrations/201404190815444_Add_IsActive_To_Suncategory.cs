namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsActive_To_Suncategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CatalogSecondCategory", "isActive", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CatalogSecondCategory", "isActive");
        }
    }
}
