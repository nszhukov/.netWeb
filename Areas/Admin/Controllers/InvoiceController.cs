using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/invoice")]
    public class InvoiceController : Controller
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;

        public InvoiceController(EcommerceContext db, SQLiteConnectorService dbSQL, ILogger<InvoiceController> logger)
        {
            this.db = db;
            this.dbSQL = dbSQL;
            logger.LogInformation("InvoiceController");
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            //ViewBag.invoices = db.Invoices.OrderByDescending(i => i.Id).ToList();
            ViewBag.invoices = dbSQL.Invoices.OrderByDescending(i => i.Id).ToList();

            return View();
        }

        [HttpGet]
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            //ViewBag.invoice = db.Invoices.Find(id);
            ViewBag.invoice = dbSQL.Invoices.Find(i => i.Id == id);

            return View("Details");
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(int id)
        {
            //Invoice invoice = db.Invoices.Find(id);
            Invoice invoice = dbSQL.Invoices.Find(i => i.Id == id);

            invoice.Status = 2;
            //db.SaveChanges();
            dbSQL.UpdateInvoices(invoice);

            return RedirectToAction("Index", "Invoice", new { area = "admin" });
        }
    }
}
