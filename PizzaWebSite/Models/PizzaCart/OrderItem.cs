using System.Collections.Generic;

namespace PizzaWebSite.Models.PizzaCart
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int PizzaId { get; set; }

        public virtual List<Topping> Toppings { get; set; }

        public int UserId { get; set; }
    }
}