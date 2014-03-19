namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addArticlesSectionsSubsections : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Articles", new[] { "SubsectionId" });
            CreateTable(
                "dbo.ArticleSubsections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        SectionId = c.Int(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleSections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.ArticleSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Articles", "SubsectionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ArticleSubsections", new[] { "SectionId" });
            DropIndex("dbo.Articles", new[] { "SubsectionId" });
            DropForeignKey("dbo.ArticleSubsections", "SectionId", "dbo.ArticleSections");
            DropForeignKey("dbo.Articles", "SubsectionId", "dbo.ArticleSubsections");
            DropTable("dbo.ArticleSections");
            DropTable("dbo.ArticleSubsections");
            CreateIndex("dbo.Articles", "SubsectionId");
            AddForeignKey("dbo.Articles", "SubsectionId", "dbo.Subsection", "Id", cascadeDelete: true);
        }
    }
}
