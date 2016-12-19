namespace PizzaWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedorderitem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PizzaToppings", newName: "ToppingPizzas");
            DropPrimaryKey("dbo.ToppingPizzas");
            AddColumn("dbo.OrderItems", "Pizza_PizzaId", c => c.Int());
            AddPrimaryKey("dbo.ToppingPizzas", new[] { "Topping_ToppingId", "Pizza_PizzaId" });
            CreateIndex("dbo.OrderItems", "Pizza_PizzaId");
            AddForeignKey("dbo.OrderItems", "Pizza_PizzaId", "dbo.Pizzas", "PizzaId");
            DropColumn("dbo.OrderItems", "PizzaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "PizzaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderItems", "Pizza_PizzaId", "dbo.Pizzas");
            DropIndex("dbo.OrderItems", new[] { "Pizza_PizzaId" });
            DropPrimaryKey("dbo.ToppingPizzas");
            DropColumn("dbo.OrderItems", "Pizza_PizzaId");
            AddPrimaryKey("dbo.ToppingPizzas", new[] { "Pizza_PizzaId", "Topping_ToppingId" });
            RenameTable(name: "dbo.ToppingPizzas", newName: "PizzaToppings");
        }
    }
}
