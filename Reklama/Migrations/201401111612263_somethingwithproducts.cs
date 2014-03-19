namespace Reklama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somethingwithproducts : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.PopularProduct", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
            CreateIndex("dbo.PopularProduct", "ProductId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PopularProduct", new[] { "ProductId" });
            DropForeignKey("dbo.PopularProduct", "ProductId", "dbo.Product");
        }
    }
}
