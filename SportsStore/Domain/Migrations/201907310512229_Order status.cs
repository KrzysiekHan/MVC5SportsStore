namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orderstatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
            AddColumn("dbo.OrderHeaders", "OrderStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderHeaders", "OrderStatusId");
            AddForeignKey("dbo.OrderHeaders", "OrderStatusId", "dbo.OrderStatus", "OrderStatusId", cascadeDelete: true);
            DropColumn("dbo.OrderHeaders", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderHeaders", "Status", c => c.String());
            DropForeignKey("dbo.OrderHeaders", "OrderStatusId", "dbo.OrderStatus");
            DropIndex("dbo.OrderHeaders", new[] { "OrderStatusId" });
            DropColumn("dbo.OrderHeaders", "OrderStatusId");
            DropTable("dbo.OrderStatus");
        }
    }
}
