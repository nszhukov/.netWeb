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
    [Route("admin/customer")]
    public class CustomerController : Controller
    {
        private EcommerceContext db = new EcommerceContext();
        private SQLiteConnectorService dbSQL;

        public CustomerController(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            //ViewBag.customers = db.Accounts.Where(a => a.RoleAccounts.FirstOrDefault().RoleId == 2).ToList();
            ViewBag.customers = dbSQL.Accounts.Where(a => a.RoleAccounts.FirstOrDefault().RoleId == 2).ToList();

            return View();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            //var customer = db.Accounts.Find(id);
            var customer = dbSQL.Accounts.Find(a => a.Id == id);

            return View("edit", customer);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Account account)
        {
            //var customer = db.Accounts.Find(id);
            var customer = dbSQL.Accounts.Find(c => c.Id == id);

            if (!string.IsNullOrEmpty(account.Password))
            {
                customer.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            customer.FullName = account.FullName;
            customer.Email = account.Email;
            customer.Address = account.Address;
            customer.Phone = account.Phone;
            customer.Status = account.Status;
            //db.SaveChanges();
            dbSQL.UpdateAccounts(customer);

            return RedirectToAction("Index", "Customer", new { area = "admin" });
        }
    }
}
