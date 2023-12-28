using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/photo")]
    public class PhotoController : Controller
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;
        private IHostingEnvironment ihostingEnvironment;

        public PhotoController(EcommerceContext db, SQLiteConnectorService dbSQL, IHostingEnvironment ihostingEnvironment)
        {
            this.db = db;
            this.dbSQL = dbSQL;
            this.ihostingEnvironment = ihostingEnvironment;
        }

        [Route("index/{id}")]
        public IActionResult Index(int id)
        {
            ViewBag.Product = dbSQL.Products.Find(p => p.Id == id);
            ViewBag.Photos = dbSQL.Photos.Where(p => p.ProductId == id).ToList();
            return View();
        }

        [HttpGet]
        [Route("add/{id}")]
        public IActionResult Add(int id)
        {
            ViewBag.Product = dbSQL.Products.Find(p => p.Id == id);

            var photo = new Photo
            {
                ProductId = id
            };

            return View("Add", photo);
        }

        [HttpPost]
        [Route("add/{productId}")]
        public IActionResult Add(int productId, Photo photo, IFormFile fileUpload)
        {
            var fileName = DateTime.Now.ToString("MMddyyyyhhmmss") + fileUpload.FileName;
            var path = Path.Combine(this.ihostingEnvironment.WebRootPath, "products", fileName);
            var stream = new FileStream(path, FileMode.Create);
            fileUpload.CopyToAsync(stream);

            photo.Name = fileName;
            //db.Photos.Add(photo);
            //db.SaveChanges();
            dbSQL.AddPhoto(photo);
            dbSQL.UpdatePhotos();

            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }

        [HttpGet]
        [Route("delete/{id}/productId")]
        public IActionResult Delete(int id, int productId)
        {
            var photo = dbSQL.Photos.Find(p => p.Id == id);
            //db.Photos.Remove(photo);
            //db.SaveChanges();
            dbSQL.DeletePhoto(photo);
            dbSQL.UpdatePhotos();

            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }

        [HttpGet]
        [Route("edit/{id}/productId")]
        public IActionResult Edit(int id, int productId)
        {
            ViewBag.Product = dbSQL.Products.Find(p => p.Id == productId);
            var photo = dbSQL.Photos.Find(p => p.Id == id);

            return View("Edit", photo);
        }

        [HttpPost]
        [Route("edit/{id}/productId")]
        public IActionResult Edit(int photoId, int productId, Photo photo, IFormFile fileUpload)
        {
            var currentPhoto = dbSQL.Photos.Find(p => p.Id == photo.Id);
            if (fileUpload != null && !string.IsNullOrEmpty(fileUpload.FileName))
            {
                var fileName = DateTime.Now.ToString("MMddyyyyhhmmss") + fileUpload.FileName;
                var path = Path.Combine(this.ihostingEnvironment.WebRootPath, "products", fileName);
                var stream = new FileStream(path, FileMode.Create);

                fileUpload.CopyToAsync(stream);
                currentPhoto.Name = fileName;
            }

            currentPhoto.Status = photo.Status;
            currentPhoto.Featured = photo.Featured;
            db.SaveChanges();

            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }

        [HttpGet]
        [Route("setfeatured/{id}/{productId}")]
        public IActionResult SetFeatured(int id, int productId)
        {
            var product = dbSQL.Products.Find(p => p.Id == productId);
            product.Photos.ToList().ForEach(p =>
            {
                p.Featured = false;
                db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            });

            var photo = dbSQL.Photos.Find(p => p.Id == id);
            photo.Featured = true;
            db.SaveChanges();

            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }
    }
}
