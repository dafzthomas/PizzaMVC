using System.Collections.Generic;

namespace PizzaWebSite.Models.PizzaCart
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public Pizza Pizza { get; set; }

        public List<Topping> Toppings { get; set; }

        public int UserId { get; set; }
    }
}