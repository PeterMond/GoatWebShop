using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using GoatWebShop.Models;

namespace GoatWebShop.Controllers
{
    public class ProductsApiController : ApiController
    {
        private ShopEntities db = new ShopEntities();

        // GET: api/ProductsApi
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/ProductsApi/GetProductsFromCategory/1
        [Route("api/ProductsApi/GetProductsFromCategory/{category}")]
        [HttpGet]
        public IQueryable<Product> GetProductsFromCat(int category)
        {
            return db.Products.Where(p => p.Category_ID == category);
        }

        // GET: api/ProductsApi/GetProductsSorted/1
        [Route("api/ProductsApi/GetProductsSorted/{sortOption}")]
        [HttpGet]
        public IQueryable<Product> GetProductsSorted(int sortOption)
        {
            IQueryable<Product> products;
            switch (sortOption)
            {
                case 1:
                    products = db.Products.OrderBy(p => p.Price);
                    break;

                case 2:
                    products = db.Products.OrderByDescending(p => p.Price);
                    break;

                case 3:
                    products = db.Products.OrderBy(p => p.Name);
                    break;

                case 4:
                    products = db.Products.OrderByDescending(p => p.Name);
                    break;

                case 5:
                    products = db.Products.OrderByDescending(p => p.OrderRows.Count());
                    break;

                case 6:
                    products = db.Products.OrderByDescending(p => p.AmountInStock).Where(p => p.AmountInStock > 0);
                    break;

                default:
                    products = db.Products;
                    break;
            }
            return products;
        }


        // GET: api/ProductsApi/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/ProductsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/ProductsApi
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
        }

        // DELETE: api/ProductsApi/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}