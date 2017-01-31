namespace PizzaWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class didntneedtoppingsonorderitem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Toppings", "OrderItem_OrderItemId1", "dbo.OrderItems");
            DropIndex("dbo.Toppings", new[] { "OrderItem_OrderItemId1" });
            DropColumn("dbo.Toppings", "OrderItem_OrderItemId1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toppings", "OrderItem_OrderItemId1", c => c.Int());
            CreateIndex("dbo.Toppings", "OrderItem_OrderItemId1");
            AddForeignKey("dbo.Toppings", "OrderItem_OrderItemId1", "dbo.OrderItems", "OrderItemId");
        }
    }
}
