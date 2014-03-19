namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xzChtoOnoXo4et : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.RealtyComment", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
            CreateIndex("dbo.RealtyComment", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RealtyComment", new[] { "UserId" });
            DropForeignKey("dbo.RealtyComment", "UserId", "dbo.UserProfile");
        }
    }
}
