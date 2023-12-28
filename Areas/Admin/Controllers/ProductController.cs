using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Areas.Admin.Models.ViewModels;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;

        public ProductController(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            //ViewBag.Products = db.Products.ToList();
            ViewBag.Products = dbSQL.Products.ToList();
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Product = new Product();
            productViewModel.Categories = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            //var categories = db.Categories.ToList();
            var categories = dbSQL.Categories.ToList();
            foreach (var category in categories)
            {
                var group = new SelectListGroup { Name = category.Name };

                if (category.InverseParent != null && category.InverseParent.Count > 0)
                {
                    foreach (var subCategory in category.InverseParent)
                    {
                        var selectListItem = new SelectListItem
                        {
                            Text = subCategory.Name,
                            Value = subCategory.Id.ToString(),
                            Group = group
                        };
                        productViewModel.Categories.Add(selectListItem);
                    }
                }
            }

            return View("Add", productViewModel);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            db.Products.Add(productViewModel.Product);
            db.SaveChanges();
            //dbSQL.AddProduct(productViewModel.Product);
            //dbSQL.UpdateProducts();
            dbSQL.UpdateProducts();

            // Create default photo for new product
            var defaultPhoto = new Photo
            {
                Name = "no-image.jpg",
                Status = true,
                ProductId = productViewModel.Product.Id,
                Featured = true
            };
            db.Photos.Add(defaultPhoto);
            db.SaveChanges();
            //dbSQL.AddPhoto(defaultPhoto);
            //dbSQL.UpdatePhotos();
            dbSQL.UpdatePhotos();

            return RedirectToAction("index", "product", new { area = "admin" });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //var product = db.Products.Find(id);
                var product = dbSQL.Products.Find(p => p.Id == id);
                db.Products.Remove(product);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                //ViewBag.Error = e.Message;
                TempData["error"] = e.Message;
            }

            return RedirectToAction("index", "product", new { area = "admin" });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var productViewModel = new ProductViewModel();
            //productViewModel.Product = db.Products.Find(id);
            productViewModel.Product = dbSQL.Products.Find(p => p.Id == id);
            productViewModel.Categories = new List<SelectListItem>();

            //var categories = db.Categories.ToList();
            var categories = dbSQL.Categories.ToList();
            foreach (var category in categories)
            {
                var group = new SelectListGroup { Name = category.Name };

                if (category.InverseParent != null && category.InverseParent.Count > 0)
                {
                    foreach (var subCategory in category.InverseParent)
                    {
                        var selectListItem = new SelectListItem
                        {
                            Text = subCategory.Name,
                            Value = subCategory.Id.ToString(),
                            Group = group
                        };
                        productViewModel.Categories.Add(selectListItem);
                    }
                }
            }

            return View("Edit", productViewModel);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, ProductViewModel productViewModel)
        {
            db.Entry(productViewModel.Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            dbSQL.UpdateProducts();

            return RedirectToAction("index", "product", new { area = "admin" });
        }
    }
}
