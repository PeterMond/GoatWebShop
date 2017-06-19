using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoatWebShop.Models;
using Microsoft.AspNet.Identity;

namespace GoatWebShop.Controllers
{
    [Authorize]
    public class MyOrdersController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: MyOrders
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var orders = db.Orders.Where(o => o.UserId == userId).Include(o => o.OrderStatu).ToList();

            //if (!orders.Any())
            //{
            //    return View();
            //}
            return View(orders);
        }

        // GET: MyOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            // check if the user is authorised to view this page
            if (order.UserId == User.Identity.GetUserId())
            {
                return View(order);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }

    }
}
