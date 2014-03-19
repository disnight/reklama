namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagerealty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Realty", "IsDisplayPhone", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Realty", "IsDisplayPhone");
        }
    }
}
