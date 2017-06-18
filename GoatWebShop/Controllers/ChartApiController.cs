using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GoatWebShop.Models;
using Microsoft.AspNet.Identity;

namespace GoatWebShop.Controllers
{
    public class ChartApiController : ApiController
    {
        private ShopEntities db = new ShopEntities();

        public string GetUserId()
        {
            // werkt niet
            var userId = "";
            if (false)
            {
                userId = User.Identity.GetUserId();
            }
            else
            {
                userId = "64e11db9-ec5f-4549-a95d-7bef312536cb";
            }
            
            return userId;
        }


        public Order GetOrCreateOrder(string customerId)
        {
            var order = db.Orders.Where(o => o.SessionUserId == customerId).FirstOrDefault();

            if (order != null)
            {
                return order;
            }

            Order newOrder = new Order { SessionUserId = customerId, Created = DateTime.Now};
            db.Orders.Add(newOrder);
            db.SaveChanges();

            return newOrder;
        }


        // GET: api/ChartApi/GetChart/
        [Route("api/ChartApi/GetChart/{userid}")]
        [HttpGet]
        public IHttpActionResult GetChart(string userId)
        {
            //Guid tempCartId = Guid.NewGuid();
            userId = GetUserId();
            var chart = db.Orders.Where(o => o.OrderStatu.Status == "Chart").Where(o => o.UserId == userId || o.SessionUserId == userId);

            return Ok(chart.First());
        }


        // GET: api/ChartApi/AddProductToChart/1/1
        [Route("api/ChartApi/AddProductToChart/{customerid}/{productid}")]
        [HttpGet]
        public IHttpActionResult AddProductToChart(string customerId, int productId)
        {
            var order = GetOrCreateOrder(customerId);

            var product = db.Products.Find(productId);

            if (product != null)
            {
                var orderRow = new OrderRow
                {
                    Order_ID = order.ID,
                    Product_ID = productId, 
                    Amount = 1,
                    //Price = product.Price;
                };
                db.OrderRows.Add(orderRow);
                db.SaveChanges();

                return Ok();
            }

            return NotFound();
        }








        // GET: api/ChartApi
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: api/ChartApi/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/ChartApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.ID)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ChartApi
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.ID }, order);
        }

        // DELETE: api/ChartApi/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.ID == id) > 0;
        }
    }
}