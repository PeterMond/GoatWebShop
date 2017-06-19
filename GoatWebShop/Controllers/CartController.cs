using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoatWebShop.Models;
using Microsoft.AspNet.Identity;

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

            return View();
        }

        [Authorize]
        public ActionResult CheckOut()
        {
            var userId = User.Identity.GetUserId();
            var sessionUserID = Session["guid"].ToString();
            var orders = db.Orders.Where(o => o.SessionUserId == sessionUserID).FirstOrDefault();

            if (orders != null)
            {
                orders.OrderStatus_id = 2;
                orders.UserId = userId;
                db.SaveChanges();

                return Redirect("/myorders");
            }

            return RedirectToAction("Index");
        }
    }
}