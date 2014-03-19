namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddarticleConfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slogan = c.String(maxLength: 255),
                        Best1Id = c.Int(),
                        Best2Id = c.Int(),
                        Best3Id = c.Int(),
                        Best4Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Best1Id)
                .ForeignKey("dbo.Articles", t => t.Best2Id)
                .ForeignKey("dbo.Articles", t => t.Best3Id)
                .ForeignKey("dbo.Articles", t => t.Best4Id)
                .Index(t => t.Best1Id)
                .Index(t => t.Best2Id)
                .Index(t => t.Best3Id)
                .Index(t => t.Best4Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ArticleConfig", new[] { "Best4Id" });
            DropIndex("dbo.ArticleConfig", new[] { "Best3Id" });
            DropIndex("dbo.ArticleConfig", new[] { "Best2Id" });
            DropIndex("dbo.ArticleConfig", new[] { "Best1Id" });
            DropForeignKey("dbo.ArticleConfig", "Best4Id", "dbo.Articles");
            DropForeignKey("dbo.ArticleConfig", "Best3Id", "dbo.Articles");
            DropForeignKey("dbo.ArticleConfig", "Best2Id", "dbo.Articles");
            DropForeignKey("dbo.ArticleConfig", "Best1Id", "dbo.Articles");
            DropTable("dbo.ArticleConfig");
        }
    }
}
