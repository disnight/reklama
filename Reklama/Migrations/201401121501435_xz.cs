namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xz : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PopularAnnoucements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnoucementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.PopularAnnouncements");
        }
    }
}
