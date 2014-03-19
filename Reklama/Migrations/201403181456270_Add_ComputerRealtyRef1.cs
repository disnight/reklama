namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ComputerRealtyRef1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ComputerRealtyRefs", newName: "ComputerRealtyRef");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ComputerRealtyRef", newName: "ComputerRealtyRefs");
        }
    }
}
