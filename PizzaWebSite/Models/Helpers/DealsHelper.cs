using PizzaWebSite.Models.PizzaCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWebSite.Models.Helpers
{
    class DealsHelper
    {
        // Deal 1: 2 Large Pizzas, 20% off

        // Deal 2: 1 Small, 1 Medium Pizza 10% off

        // Deal 3: 5 Large pizzas, 60% off

        private DealsHelper()
        {
            currentOrder = SessionHelper.Current.Cart;

            dealOneOrder = new Order();
            dealOneOrder.OrderItems.Add(new OrderItem().Pizza.Size = "large");
            dealOneOrder.OrderItems.Add(new OrderItem().Pizza.Size = "large");
        }

        private static Order currentOrder;
        private static Order dealOneOrder;

        //public static int CheckDeals()
        //{
              
        //}
    }
}
