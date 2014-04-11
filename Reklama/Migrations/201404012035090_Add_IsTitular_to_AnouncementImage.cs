namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsTitular_to_AnouncementImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnnouncementImages", "IsTitular", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnnouncementImages", "IsTitular");
        }
    }
}
