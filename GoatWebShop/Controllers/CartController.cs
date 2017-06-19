using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoatWebShop.Models;

namespace GoatWebShop.Controllers
{
    public class CartController : Controller
    {
        private ShopEntities db = new ShopEntities();


        // GET: Chart
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

            //var userId = guid;
            //var chart = db.Orders.Where(o => o.OrderStatu.Status == "Chart").Where(o => o.UserId == userId || o.SessionUserId == userId);

            return View();
        }
    }
}