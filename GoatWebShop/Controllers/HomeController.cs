using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoatWebShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["guid"] != null)
            {
                ViewBag.guid = Session["guid"];
            }
            else
            {
                Guid guid = Guid.NewGuid();
                Session["guid"] = guid;
                ViewBag.guid = guid;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}