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
using Microsoft.AspNet.Identity;

namespace PizzaWebSite.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            List<string> TempPizzaNames = new List<string>();
            List<Pizza> Pizzas = new List<Pizza>();

            foreach(var pizza in DatabaseContextHelper.Db.Pizzas.ToList())
            {
                var pizzaFound = TempPizzaNames.Contains(pizza.Name);

                if (pizzaFound == false)
                {
                    TempPizzaNames.Add(pizza.Name);
                    Pizzas.Add(pizza);
                }
            }

            ViewBag.AllPizzas = DatabaseContextHelper.Db.Pizzas.ToList();
            return View(Pizzas);
        }

        public ActionResult Cart()
        {
            if (SessionHelper.Current.Cart.OrderItems.ToList().Count > 0)
            {
                return View(SessionHelper.Current.Cart.OrderItems.ToList());
            } else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult PizzaCustomise(int? id)
        {
            ViewBag.Message = "Customise Pizza.";
            ViewBag.AllToppings = DatabaseContextHelper.Db.Toppings.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = DatabaseContextHelper.Db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        [Authorize]
        public ActionResult Confirm()
        {
            if (SessionHelper.Current.Cart.OrderItems.ToList().Count > 0)
            {
                return View(SessionHelper.Current.Cart.OrderItems.ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public ActionResult Complete()
        {
            ViewBag.completedOrder = SessionHelper.Current.Cart;
            SessionHelper.Current.Cart = new Order();
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "PizzaId, ToppingId")] Pizza pizza, List<Topping> toppings)
        {
            OrderItem tempPizza = new OrderItem();
            tempPizza.Pizza = DatabaseContextHelper.Db.Pizzas.Find(pizza.PizzaId);

            if (SessionHelper.Current.Cart.OrderItems == null)
            {
                SessionHelper.Current.Cart.OrderItems = new List<OrderItem>();
            }

            var test = pizza;

            //tempPizza.ExtraToppings.Add(pizza.Toppings);

            SessionHelper.Current.Cart.OrderItems.Add(tempPizza);

            return RedirectToAction("Cart");

        }

        [HttpPost]
        public ActionResult Remove([Bind(Include = "PizzaId")] Pizza pizza)
        {
            var itemToRemove = SessionHelper.Current.Cart.OrderItems.Single(r => r.Pizza.PizzaId == pizza.PizzaId);

            SessionHelper.Current.Cart.OrderItems.Remove(itemToRemove);

            return RedirectToAction("Cart");
        }

        [Authorize]
        [HttpPost]
        public ActionResult SubmitOrder()
        {
            SessionHelper.Current.Cart.UserId = User.Identity.GetUserId();

            foreach(var item in SessionHelper.Current.Cart.OrderItems)
            {
                SessionHelper.Current.Cart.Price += item.Pizza.Price;
            }

            DatabaseContextHelper.Db.Orders.Add(SessionHelper.Current.Cart);
            DatabaseContextHelper.Db.SaveChanges();

            return RedirectToAction("Complete");
        }
    }
}