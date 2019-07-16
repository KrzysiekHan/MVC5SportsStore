namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAdress = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        Street = c.String(nullable: false),
                        BuildingNr = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.String(),
                        Country = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.OrderDetails", "OrderHeader_Id", "dbo.OrderHeaders");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderHeader_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Products");
            DropTable("dbo.OrderHeaders");
            DropTable("dbo.OrderDetails");
        }
    }
}
