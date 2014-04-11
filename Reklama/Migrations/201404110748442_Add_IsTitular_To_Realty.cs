namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsTitular_To_Realty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealtyPhoto", "IsTitular", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealtyPhoto", "IsTitular");
        }
    }
}
