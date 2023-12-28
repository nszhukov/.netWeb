 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Ecommerce.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;

        public HomeController(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            ViewBag.isHome = true;

            //var featuredProducts = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status && p.Featured).ToList();
            List<Product> featuredProducts = dbSQL.Products.OrderByDescending(p => p.Id).Where(p => p.Status && p.Featured).ToList();
            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.CountFeaturedProducts = featuredProducts.Count;

            //ViewBag.LatestProducts = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status).Take(6).ToList();
            ViewBag.LatestProducts = dbSQL.Products.OrderByDescending(p => p.Id).Where(p => p.Status).Take(10).ToList();

            return View();
        }
    }
}
