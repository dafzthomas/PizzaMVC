using System.Collections.Generic;

namespace PizzaWebSite.Models.PizzaCart
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}