namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsNewToSubsection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subsection", "IsNew", c => c.Boolean(nullable: false), new { Default = false });
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subsection", "IsNew");
        }
    }
}
