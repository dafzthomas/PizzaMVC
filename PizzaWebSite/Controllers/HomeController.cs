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
using System.Security.Principal;

namespace PizzaWebSite.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            List<string> TempPizzaNames = new List<string>();
            List<Pizza> Pizzas = new List<Pizza>();
            ViewBag.AllPizzas = DatabaseContextHelper.Db.Pizzas.ToList();

            foreach (var pizza in ViewBag.AllPizzas)
            {
                var pizzaFound = TempPizzaNames.Contains(pizza.Name);

                if (pizzaFound == false)
                {
                    TempPizzaNames.Add(pizza.Name);
                    Pizzas.Add(pizza);
                }
            }
            
            return View(Pizzas);
        }

        public ActionResult Cart(int? id)
        {
            if (id != null)
            {
                CartHelper.ReorderOrder(Convert.ToInt32(id), SessionHelper.Current.Cart);
            }

            ViewBag.Order = SessionHelper.Current.Cart;
            ViewBag.User = DatabaseContextHelper.Db.Users.Find(User.Identity.GetUserId());

            return View(SessionHelper.Current.Cart.OrderItems.ToList());
        }

        [Authorize]
        public ActionResult Confirm()
        {
            ViewBag.Order = SessionHelper.Current.Cart;
            ViewBag.User = DatabaseContextHelper.Db.Users.Find(User.Identity.GetUserId());

            return View(SessionHelper.Current.Cart.OrderItems.ToList());
        }

        [Authorize]
        public ActionResult Complete()
        {
            ViewBag.Order = SessionHelper.Current.Cart;
            ViewBag.User = DatabaseContextHelper.Db.Users.Find(User.Identity.GetUserId());

            return View();
        }

        public ActionResult PizzaCustomise(int? id)
        {
            ViewBag.Message = "Customise Pizza.";
            ViewBag.AllToppings = new List<Topping>();

            var allToppings = DatabaseContextHelper.Db.Toppings.ToList();
            Pizza pizza = DatabaseContextHelper.Db.Pizzas.Find(id);

            foreach(var topping in allToppings)
            {
                if (pizza.Size == topping.Size)
                {
                    ViewBag.AllToppings.Add(topping);
                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(pizza);
        }

        [HttpPost]
        public ActionResult Add(Pizza pizza, FormCollection form)
        {
            string selectedToppings = form["ToppingId"];

            CartHelper.Add(pizza.PizzaId, selectedToppings, SessionHelper.Current.Cart);

            return RedirectToAction("Cart");

        }

        [HttpPost]
        public ActionResult Remove([Bind(Include = "PizzaId")] Pizza pizza)
        {
            CartHelper.Remove(pizza, SessionHelper.Current.Cart);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult ApplyDelivery([Bind(Include = "Delivery")] bool Delivery)
        {
            CartHelper.ApplyDelivery(Delivery, SessionHelper.Current.Cart);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult CheckDeal([Bind(Include = "voucher")] string voucher)
        {
            CartHelper.AddDealToCart(voucher, SessionHelper.Current.Cart);
            return RedirectToAction("Confirm");
        }

        [Authorize]
        [HttpPost]
        public ActionResult SubmitOrder()
        {
            string userId = User.Identity.GetUserId();
            CartHelper.Submit(userId, SessionHelper.Current.Cart);
            CartHelper.ResetCart(SessionHelper.Current.Cart);

            return RedirectToAction("Complete");
        }
    }
}