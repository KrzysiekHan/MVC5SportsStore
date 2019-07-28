namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserRoletouserentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserRole", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserRole");
        }
    }
}
