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

namespace GoatWebShop.Controllers
{
    public class CategoriesApiController : ApiController
    {
        private ShopEntities db = new ShopEntities();

        // GET: api/CategoriesApi
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }

    }
}