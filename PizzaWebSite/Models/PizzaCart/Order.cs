using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebSite.Models.PizzaCart
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Price { get; set; } = 0;

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}