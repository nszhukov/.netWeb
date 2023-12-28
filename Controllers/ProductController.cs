using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using X.PagedList;

namespace Ecommerce.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private EcommerceContext db = new EcommerceContext();
        private SQLiteConnectorService dbSQL;

        public ProductController(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            //var product = db.Products.Find(id);
            var product = dbSQL.Products.Find(p => p.Id == id);

            var featuredPhoto = product.Photos.SingleOrDefault(p => p.Status && p.Featured);
            ViewBag.Product = product;
            ViewBag.FeaturedPhoto = featuredPhoto == null ? "no-image.jpg" : featuredPhoto.Name;
            ViewBag.ProductImages = product.Photos.Where(p => p.Status).ToList();

            //ViewBag.RelatedProducts = db.Products.Where(p => p.CategoryId == product.CategoryId && p.Id != id && p.Status);
            ViewBag.RelatedProducts = dbSQL.Products.Where(p => p.CategoryId == product.CategoryId && p.Id != id && p.Status);

            return View("Details");
        }

        [Route("category/{id}")]
        public IActionResult Category(int id, int? page)
        {
            var pageNumber = page ?? 1;

            //Category category = db.Categories.Find(id);
            Category category = dbSQL.Categories.Find(c => c.Id == id);

            ViewBag.Category = category;
            ViewBag.CountProducts = category.Products.Count(p => p.Status);
            ViewBag.Products = category.Products.Where(p => p.Status).ToList().ToPagedList(pageNumber, 6);

            return View("Category");
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword, int? page)
        {
            var pageNumber = page ?? 1;
            //var products = db.Products.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) && p.Status).ToList();
            var products = dbSQL.Products.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) && p.Status).ToList();
            ViewBag.Keyword = keyword;
            ViewBag.CountProducts = products.Count;
            ViewBag.Products = products.ToPagedList(pageNumber, 6);

            return View("Search");
        }
    }
}
