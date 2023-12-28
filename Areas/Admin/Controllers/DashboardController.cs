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
    [Route("admin/dashboard")]
    public class DashboardController : Controller
    {
        private EcommerceContext db = new EcommerceContext();
        private SQLiteConnectorService dbSQL;

        public DashboardController(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        public IActionResult Index()
        {
            /*ViewBag.countInvoices = db.Invoices.Count(i => i.Status == 1);
            ViewBag.countProducts = db.Products.Count();
            ViewBag.countCustomers = db.RoleAccounts.Count(ra => ra.RoleId == 2);
            ViewBag.countCategories = db.Categories.Count(c => c.ParrentId == null);*/

            ViewBag.countInvoices = dbSQL.Invoices.Count(i => i.Status == 1);
            ViewBag.countProducts = dbSQL.Products.Count();
            ViewBag.countCustomers = dbSQL.RoleAccounts.Count(ra => ra.RoleId == 2);
            ViewBag.countCategories = dbSQL.Categories.Count(c => c.ParrentId == null);

            return View();
        }
    }
}

