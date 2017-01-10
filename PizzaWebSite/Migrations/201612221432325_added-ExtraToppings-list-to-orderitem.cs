namespace PizzaWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedExtraToppingslisttoorderitem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ToppingPizzas", newName: "PizzaToppings");
            DropForeignKey("dbo.Toppings", "OrderItem_OrderItemId", "dbo.OrderItems");
            DropPrimaryKey("dbo.PizzaToppings");
            AddColumn("dbo.Toppings", "OrderItem_OrderItemId1", c => c.Int());
            AddPrimaryKey("dbo.PizzaToppings", new[] { "Pizza_PizzaId", "Topping_ToppingId" });
            CreateIndex("dbo.Toppings", "OrderItem_OrderItemId1");
            AddForeignKey("dbo.Toppings", "OrderItem_OrderItemId", "dbo.OrderItems", "OrderItemId");
            AddForeignKey("dbo.Toppings", "OrderItem_OrderItemId1", "dbo.OrderItems", "OrderItemId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toppings", "OrderItem_OrderItemId1", "dbo.OrderItems");
            DropForeignKey("dbo.Toppings", "OrderItem_OrderItemId", "dbo.OrderItems");
            DropIndex("dbo.Toppings", new[] { "OrderItem_OrderItemId1" });
            DropPrimaryKey("dbo.PizzaToppings");
            DropColumn("dbo.Toppings", "OrderItem_OrderItemId1");
            AddPrimaryKey("dbo.PizzaToppings", new[] { "Topping_ToppingId", "Pizza_PizzaId" });
            AddForeignKey("dbo.Toppings", "OrderItem_OrderItemId", "dbo.OrderItems", "OrderItemId");
            RenameTable(name: "dbo.PizzaToppings", newName: "ToppingPizzas");
        }
    }
}
