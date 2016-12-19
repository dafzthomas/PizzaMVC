using PizzaWebSite.Models.PizzaCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWebSite.Models.Helpers
{
    class DatabaseContextHelper
    {
        public static PizzaOrderContext Db = new PizzaOrderContext();
    }
}
