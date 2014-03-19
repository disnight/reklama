namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsNewFieldToCatalogSecondCategpry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CatalogSecondCategory", "isNew", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CatalogSecondCategory", "isNew");
        }
    }
}
