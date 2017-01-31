namespace PizzaWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeditemstoorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "Delivery", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Delivery");
            DropColumn("dbo.Orders", "Discount");
        }
    }
}
