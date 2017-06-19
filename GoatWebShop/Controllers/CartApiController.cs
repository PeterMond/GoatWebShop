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
    public class CartApiController : ApiController
    {
        private ShopEntities db = new ShopEntities();

        // get the current order or create a new order
        public Order GetOrder(string customerId, bool allowCreateNewOrder = false)
        {
            var order = db.Orders.Where(o => o.SessionUserId == customerId).FirstOrDefault();

            if (order == null && allowCreateNewOrder)
            {
                Order newOrder = new Order
                {
                    SessionUserId = customerId,
                    Created = DateTime.Now,
                    OrderNumber = "Order-" + DateTime.Now.ToShortDateString(),
                    OrderStatus_id = 1
                };
                db.Orders.Add(newOrder);
                db.SaveChanges();

                return newOrder;
            }

            return order;
        }


        // GET: api/CartApi/GetCart/
        [Route("api/CartApi/GetCart/{userid}")]
        [HttpGet]
        public IHttpActionResult GetCart(string userId)
        {
            var Cart = db.Orders.Where(o => o.OrderStatu.Status == "Cart").Where(o => o.UserId == userId || o.SessionUserId == userId);

            return Ok(Cart.First());
        }


        // GET: api/CartApi/AddProductToCart/1/1/1
        [Route("api/CartApi/AddProductToCart/{customerid}/{productid}/{amount}")]
        [HttpGet]
        public IHttpActionResult AddProductToCart(string customerId, int productId, int amount)
        {
            var order = GetOrder(customerId, true);

            var product = db.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            // Check if the order already has the product
            var orderRow = db.OrderRows.Where(o => o.Order_ID == order.ID && o.Product_ID == productId).FirstOrDefault();
            if (orderRow != null)
            {
                orderRow.Amount += amount;
                orderRow.Price += (product.Price * amount);
            }
            else
            {
                orderRow = new OrderRow
                {
                    Order_ID = order.ID,
                    Product_ID = productId,
                    Amount = amount,
                    Price = (product.Price * amount)
                };

                db.OrderRows.Add(orderRow);
            }
            db.SaveChanges();

            return Ok();

        }


        // GET: api/CartApi/RemoveProductFromCart/1/1/
        [Route("api/CartApi/RemoveProductFromCart/{customerid}/{productid}/")]
        [HttpGet]
        public IHttpActionResult RemoveProductFromCart(string customerId, int productId)
        {
            var order = GetOrder(customerId);

            var product = db.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            // Check if the order already has the product
            var orderRow = db.OrderRows.Where(o => o.Order_ID == order.ID && o.Product_ID == productId).FirstOrDefault();
            if (orderRow != null)
            {
                db.OrderRows.Remove(orderRow);
                db.SaveChanges();
            }

            return Ok();
        }


        // GET: api/CartApi/DeleteCart/1/
        [Route("api/CartApi/DeleteCart/{customerid}")]
        [HttpGet]
        public IHttpActionResult DeleteCart(string customerId)
        {
            var order = GetOrder(customerId);

            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok();
        }











        // GET: api/CartApi
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: api/CartApi/5
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

        // PUT: api/CartApi/5
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

        // POST: api/CartApi
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

        // DELETE: api/CartApi/5
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