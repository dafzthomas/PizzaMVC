using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaWebSite.Models;
using PizzaWebSite.Models.PizzaCart;
using System.Net;
using PizzaWebSite.Models.Helpers;

namespace PizzaWebSite.Controllers
{
    public class HomeController : Controller
    {

        private Models.PizzaCart.PizzaOrderContext db = new Models.PizzaCart.PizzaOrderContext();

        public ActionResult Index()
        {
            return View(db.Pizzas.ToList());
        }

        public ActionResult Cart()
        {

            ViewBag.Cart = SessionHelper.Current.Cart;
            ViewBag.OrderItems = SessionHelper.Current.Cart.OrderItems;


            var Pizzas = new List<Pizza>();

            if (ViewBag.OrderItems != null)
            {
                foreach (var item in ViewBag.OrderItems)
                {
                    Pizza pizza = db.Pizzas.Find(item.PizzaId);

                    Pizzas.Add(pizza);
                }
            }
            

            ViewBag.Pizzas = Pizzas;

            return View();
        }

        public ActionResult PizzaCustomise(int? id)
        {
            ViewBag.Message = "Customise Pizza.";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "PizzaId, Toppings")] Pizza pizza)
        {


            OrderItem tempPizza = new OrderItem();
            tempPizza.PizzaId = pizza.PizzaId;
            tempPizza.Toppings = pizza.Toppings;

            if (SessionHelper.Current.Cart.OrderItems == null)
            {
                SessionHelper.Current.Cart.OrderItems = new List<OrderItem>();
            }

            SessionHelper.Current.Cart.OrderItems.Add(tempPizza);



            return RedirectToAction("Cart");

        }

        [HttpPost]
        public ActionResult Remove([Bind(Include = "PizzaId")] OrderItem orderItem)
        {
            var itemToRemove = SessionHelper.Current.Cart.OrderItems.Single(r => r.PizzaId == orderItem.PizzaId);

            SessionHelper.Current.Cart.OrderItems.Remove(itemToRemove);

            return RedirectToAction("Cart");
        }
    }
}