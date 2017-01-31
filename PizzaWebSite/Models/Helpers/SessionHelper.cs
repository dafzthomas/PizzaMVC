using PizzaWebSite.Models.PizzaCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PizzaWebSite.Models.Helpers
{
    class SessionHelper
    {
        public SessionHelper()
        {
            Cart = new Order();
        }
        public Order Cart { get; set; }

        public static SessionHelper Current
        {
            get
            {
                SessionHelper session = (SessionHelper)HttpContext.Current.Session["PizzaCart"];
                if (session == null)
                {
                    session = new SessionHelper();
                    HttpContext.Current.Session["PizzaCart"] = session;
                }
                return session;
            }
            set
            {
                HttpContext.Current.Session["PizzaCart"] = value;
            }
        }


    }
}
