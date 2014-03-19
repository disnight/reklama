namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somewitharticlesonmainpage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainPageArticleConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Article1Id = c.Int(),
                        Article2Id = c.Int(),
                        Article3Id = c.Int(),
                        Article4Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Article1Id)
                .ForeignKey("dbo.Articles", t => t.Article2Id)
                .ForeignKey("dbo.Articles", t => t.Article3Id)
                .ForeignKey("dbo.Articles", t => t.Article4Id)
                .Index(t => t.Article1Id)
                .Index(t => t.Article2Id)
                .Index(t => t.Article3Id)
                .Index(t => t.Article4Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MainPageArticleConfigs", new[] { "Article4Id" });
            DropIndex("dbo.MainPageArticleConfigs", new[] { "Article3Id" });
            DropIndex("dbo.MainPageArticleConfigs", new[] { "Article2Id" });
            DropIndex("dbo.MainPageArticleConfigs", new[] { "Article1Id" });
            DropForeignKey("dbo.MainPageArticleConfigs", "Article4Id", "dbo.Articles");
            DropForeignKey("dbo.MainPageArticleConfigs", "Article3Id", "dbo.Articles");
            DropForeignKey("dbo.MainPageArticleConfigs", "Article2Id", "dbo.Articles");
            DropForeignKey("dbo.MainPageArticleConfigs", "Article1Id", "dbo.Articles");
            DropTable("dbo.MainPageArticleConfigs");
        }
    }
}
