namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, storeType: "money"),
                        UnitPriceDiscount = c.Decimal(nullable: false, storeType: "money"),
                        LineTotal = c.Decimal(nullable: false, storeType: "money"),
                        OrderHeader_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderHeaders", t => t.OrderHeader_Id)
                .Index(t => t.OrderHeader_Id);
            
            CreateTable(
                "dbo.OrderHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                        CustomerId = c.Int(nullable: false),
                        ShipAddress = c.String(),
                        ShipmentMethod = c.String(),
                        Status = c.String(),
                        Comment = c.String(),
                        TotalDue = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderHeader_Id", "dbo.OrderHeaders");
            DropIndex("dbo.OrderDetails", new[] { "OrderHeader_Id" });
            DropTable("dbo.OrderHeaders");
            DropTable("dbo.OrderDetails");
        }
    }
}
