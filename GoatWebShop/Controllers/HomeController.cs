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

            ViewBag.UserId = User.Identity.GetUserId();

            ViewBag.UserName = User.Identity.GetUserName();

            var UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());

            var persoon = UserManager.FindByEmail("test@test.nl");
            ViewBag.UserName2 = persoon.UserName;

            if (persoon.Roles.Any())
            {
                ViewBag.UserRole = persoon.Roles.FirstOrDefault().RoleId;
            }
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}