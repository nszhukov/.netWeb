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
    [Route("admin/slideshow")]
    public class SlideShowController : Controller
    {
        private EcommerceContext db = new EcommerceContext();
        private SQLiteConnectorService dbSQL;
        private IHostingEnvironment ihostingEnvironment;

        public SlideShowController(EcommerceContext db, SQLiteConnectorService dbSQL, IHostingEnvironment ihostingEnvironment)
        {
            this.db = db;
            this.dbSQL = dbSQL;
            this.ihostingEnvironment = ihostingEnvironment;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            //ViewBag.SlideShows = db.SlideShows.ToList();
            ViewBag.SlideShows = dbSQL.SlideShows.ToList();
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var slideShow = new SlideShow();

            return View("Add", slideShow);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(SlideShow slideShow, IFormFile photo)
        {
            var fileName = DateTime.Now.ToString("MMddyyyyhhmmss") + photo.FileName;
            var path = Path.Combine(this.ihostingEnvironment.WebRootPath, "slideshows", fileName);
            var stream = new FileStream(path, FileMode.Create);
            photo.CopyToAsync(stream);

            slideShow.Name = fileName;
            db.SlideShows.Add(slideShow);
            db.SaveChanges();

            return RedirectToAction("index", "slideshow", new { area = "admin" });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            //var slideShow = db.SlideShows.Find(id);
            var slideShow = dbSQL.SlideShows.Find(ss => ss.Id == id);
            db.SlideShows.Remove(slideShow);
            db.SaveChanges();

            return RedirectToAction("index", "slideshow", new { area = "admin" });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            //var slideShow = db.SlideShows.Find(id);
            var slideShow = dbSQL.SlideShows.Find(ss => ss.Id == id);

            return View("Edit", slideShow);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, SlideShow slideShow, IFormFile photo)
        {
            //var currentSlideShow = db.SlideShows.Find(slideShow.Id);
            var currentSlideShow = dbSQL.SlideShows.Find(ss => ss.Id == slideShow.Id);
            if (photo != null && !string.IsNullOrEmpty(photo.FileName))
            {
                var fileName = DateTime.Now.ToString("MMddyyyyhhmmss") + photo.FileName;
                var path = Path.Combine(this.ihostingEnvironment.WebRootPath, "slideshows", fileName);
                var stream = new FileStream(path, FileMode.Create);

                photo.CopyToAsync(stream);
                currentSlideShow.Name = fileName;
            }
            currentSlideShow.Status = slideShow.Status;
            currentSlideShow.Title = slideShow.Title;
            currentSlideShow.Description = slideShow.Description;
            db.SaveChanges();

            return RedirectToAction("index", "slideshow", new { area = "admin" });
        }
    }
}
