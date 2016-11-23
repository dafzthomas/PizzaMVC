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

namespace PizzaWebSite.Controllers {
    public class HomeController : Controller {

        private Models.PizzaCart.PizzaOrderContext db = new Models.PizzaCart.PizzaOrderContext();

        public ActionResult Index() {
            return View(db.Pizzas.ToList());
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
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
    }
}