namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPopularAnnoucement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.PopularAnnoucements",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnoucementId = c.Int(nullable: false),
                    })
                    .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.PopularAnnoucements");
        }
    }
}
