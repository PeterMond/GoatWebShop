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
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace GoatWebShop.Controllers
{
    [Authorize]
    public class CustomerDetailsController : Controller
    {
        private ShopEntities db = new ShopEntities();

        public bool IsAdmin()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = userManager.GetRoles(User.Identity.GetUserId());

            if (roles.Contains("Admin"))
            {
                return true;
            }
            return false;
        }

        // GET: CustomerDetails
        public ActionResult Index()
        {
            if (IsAdmin() != true)
            {
                var userId = User.Identity.GetUserId();
                var customerDetailId = db.CustomerDetails.Where(c => c.UserId.Equals(userId)).Select(c => c.ID).FirstOrDefault();

                if (customerDetailId != 0)
                {
                    return RedirectToAction("Details", new { id = customerDetailId });
                }
                else
                {
                    return RedirectToAction("Create");
                }
               
            }
            var customerDetails = db.CustomerDetails.Include(c => c.AspNetUser);
            return View(customerDetails.ToList());
        }

        // GET: CustomerDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetail customerDetail = db.CustomerDetails.Find(id);
            if (customerDetail == null)
            {
                return HttpNotFound();
            }
           
            // check if the user is authorised to view this page
            if (customerDetail.UserId == User.Identity.GetUserId()|| IsAdmin() )
            {
                return View(customerDetail);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }

        // GET: CustomerDetails/Create
        public ActionResult Create()
        {
            if (IsAdmin())
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            }
            else
            {

                // check if the user already has an account
                var userId = User.Identity.GetUserId();
                var customerDetailId = db.CustomerDetails.Where(c => c.UserId.Equals(userId)).Select(c => c.ID).FirstOrDefault();
                if (customerDetailId != 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        // POST: CustomerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,FirstName,LastName,Birthday,Adress")] CustomerDetail customerDetail)
        {
            if (ModelState.IsValid)
            {
                if (customerDetail.UserId == null)
                {
                    customerDetail.UserId = User.Identity.GetUserId();
                }

                db.CustomerDetails.Add(customerDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (IsAdmin())
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", customerDetail.UserId);
            }
            return View(customerDetail);
        }

        // GET: CustomerDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetail customerDetail = db.CustomerDetails.Find(id);
            if (customerDetail == null)
            {
                return HttpNotFound();
            }

            if (IsAdmin())
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", customerDetail.UserId);
            }

            Session["CustomerId"] = id;

            // check if the user is authorised to view this page
            if (customerDetail.UserId == User.Identity.GetUserId() || IsAdmin())
            {
                return View(customerDetail);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }

        // POST: CustomerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,FirstName,LastName,Birthday,Adress")] CustomerDetail customerDetail)
        {
            // check if the user tries to edit someone elses customerdetails
            if (customerDetail.ID != ((int)Session["CustomerId"]))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (ModelState.IsValid)
            {
                if (customerDetail.UserId == null)
                {
                    customerDetail.UserId = User.Identity.GetUserId();
                }

                db.Entry(customerDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (IsAdmin())
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", customerDetail.UserId);
            }
            
            return View(customerDetail);
        }

        [Authorize(Roles = "Admin")]
        // GET: CustomerDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetail customerDetail = db.CustomerDetails.Find(id);
            if (customerDetail == null)
            {
                return HttpNotFound();
            }
            return View(customerDetail);
        }

        [Authorize(Roles = "Admin")]
        // POST: CustomerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerDetail customerDetail = db.CustomerDetails.Find(id);
            db.CustomerDetails.Remove(customerDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
