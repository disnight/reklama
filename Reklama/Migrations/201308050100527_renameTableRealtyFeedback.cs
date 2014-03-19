namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameTableRealtyFeedback : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RealtyFeedback", new[] { "RealtyId" });
            CreateTable(
                "dbo.RealtyComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 300),
                        CreatedAt = c.DateTime(nullable: false),
                        RealtyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.RealtyId);
            
            DropTable("dbo.RealtyFeedback");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RealtyFeedback",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 300),
                        CreatedAt = c.DateTime(nullable: false),
                        RealtyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.RealtyComment", new[] { "RealtyId" });
            DropTable("dbo.RealtyComment");
            CreateIndex("dbo.RealtyFeedback", "RealtyId");
        }
    }
}
