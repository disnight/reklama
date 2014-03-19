namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ComputerRealtyRef : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComputerRealtyRefs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RealtyId = c.Int(nullable: false),
                        ComputerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Computer", t => t.ComputerId, cascadeDelete: true)
                .ForeignKey("dbo.Realty", t => t.RealtyId, cascadeDelete: true)
                .Index(t => t.ComputerId)
                .Index(t => t.RealtyId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ComputerRealtyRefs", new[] { "RealtyId" });
            DropIndex("dbo.ComputerRealtyRefs", new[] { "ComputerId" });
            DropForeignKey("dbo.ComputerRealtyRefs", "RealtyId", "dbo.Realty");
            DropForeignKey("dbo.ComputerRealtyRefs", "ComputerId", "dbo.Computer");
            DropTable("dbo.ComputerRealtyRefs");
        }
    }
}
