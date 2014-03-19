namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneToRealty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Realty", "Phone", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Realty", "Phone");
        }
    }
}
