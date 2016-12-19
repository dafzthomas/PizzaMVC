namespace PizzaWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useridtostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
        }
    }
}
