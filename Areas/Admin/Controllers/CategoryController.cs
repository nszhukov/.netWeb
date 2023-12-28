using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
    {
        private EcommerceContext db = new EcommerceContext();
        private SQLiteConnectorService dbSQL;

        public CategoryController(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            //ViewBag.categories = db.Categories.Where(c => c.Parent == null).ToList();
            //ViewBag.categories = dbSQL.Categories.Where(c => c.Parent == null).ToList();
            ViewBag.categories =  dbSQL.Categories.Where(c => c.ParrentId == null).ToList();
            ;
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var category = new Category();

            return View("Add", category);
        }
        
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Category category)
        {
            category.Parent = null;

            //db.Categories.Add(category);
            //db.SaveChanges();
            dbSQL.AddCategory(category);
            dbSQL.UpdateCategories();

            return RedirectToAction("index", "category", new { area = "admin" });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //var category = db.Categories.Find(id);
                //db.Categories.Remove(category);
                //db.SaveChanges();
                var category = dbSQL.Categories.Find(c => c.Id == id);
                dbSQL.DeleteCategorie(category);
                dbSQL.UpdateCategories();
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }

            return RedirectToAction("index", "category", new { area = "admin" });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            //var category = db.Categories.Find(id);
            var category = dbSQL.Categories.Find(c => c.Id == id);

            return View("Edit", category);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Category category)
        {
            var currentCategory = db.Categories.Find(id);

            currentCategory.Name = category.Name;
            currentCategory.Status = category.Status;
            //db.SaveChanges();
            dbSQL.UpdateCategories(currentCategory);

            return RedirectToAction("index", "category", new { area = "admin" });
        }

        [HttpGet]
        [Route("addsubcategory/{id}")]
        public IActionResult AddSubCategory(int id)
        {
            var category = new Category()
            {
                ParrentId = id
            };

            return View("AddSubCategory", category);
        }

        [HttpPost]
        [Route("addsubcategory/{categoryId}")]
        public IActionResult AddSubCategory(int categoryId, Category category)
        {
            //db.Categories.Add(category);
            //db.SaveChanges();

            dbSQL.AddCategory(category);
            dbSQL.UpdateCategories();

            return RedirectToAction("index", "category", new { area = "admin" });
        }
    }
}
