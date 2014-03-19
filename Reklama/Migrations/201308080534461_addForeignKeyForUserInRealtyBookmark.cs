namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addForeignKeyForUserInRealtyBookmark : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RealtyBookmark", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RealtyBookmark", new[] { "UserId" });
            DropForeignKey("dbo.RealtyBookmark", "UserId", "dbo.UserProfile");
        }
    }
}
