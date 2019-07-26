namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class joinorderdetailwithproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Product_ProductID", c => c.Int());
            CreateIndex("dbo.OrderDetails", "Product_ProductID");
            AddForeignKey("dbo.OrderDetails", "Product_ProductID", "dbo.Products", "ProductID");
            DropColumn("dbo.OrderDetails", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "Product_ProductID" });
            DropColumn("dbo.OrderDetails", "Product_ProductID");
        }
    }
}
