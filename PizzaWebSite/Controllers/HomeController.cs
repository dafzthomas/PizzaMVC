using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaWebSite.Models;

namespace PizzaWebSite.Controllers {
    public class HomeController : Controller {

        

        public ActionResult Index() {


            return View();
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
        public ActionResult ViewTest()
        {
            ViewBag.Message = "Your test view page.";

            return View();
        }
    }
}