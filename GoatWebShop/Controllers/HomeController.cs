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
            

            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            //string[] roleNames = { "Admin", "Member", "Moderator", "Junior", "Senior", "Customer" };
            //IdentityResult roleResult;
            //foreach (var roleName in roleNames)
            //{
            //    if (!RoleManager.RoleExists(roleName))
            //    {
            //        roleResult = RoleManager.Create(new IdentityRole(roleName));
            //    }
            //}

  


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}