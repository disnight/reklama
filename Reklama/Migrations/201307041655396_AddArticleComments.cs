namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArticleComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 1000),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ArticleComment", new[] { "ArticleId" });
            DropIndex("dbo.ArticleComment", new[] { "UserId" });
            DropForeignKey("dbo.ArticleComment", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleComment", "UserId", "dbo.UserProfile");
            DropTable("dbo.ArticleComment");
        }
    }
}
