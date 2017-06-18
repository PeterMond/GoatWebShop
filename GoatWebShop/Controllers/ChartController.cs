using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoatWebShop.Models;

namespace GoatWebShop.Controllers
{
    public class ChartController : Controller
    {
        private ShopEntities db = new ShopEntities();


        // GET: Chart
        public ActionResult Index()
        {
            var userId = "test";
            var chart = db.Orders.Where(o => o.OrderStatu.Status == "Chart").Where(o => o.UserId == userId || o.SessionUserId == userId);

            return View();
        }
    }
}