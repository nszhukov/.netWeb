using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Security;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Controllers
{
    [Route("customer")]
    public class CustomerController : Controller
    {
        private EcommerceContext db = new EcommerceContext();
        private SQLiteConnectorService dbSQL;
        private SecurityManager securityManager = new SecurityManager();
        private ILogger logger;

        public CustomerController(EcommerceContext db, SQLiteConnectorService dbSQL, ILogger<CustomerController> logger)
        {
            this.db = db;
            this.dbSQL = dbSQL;
            this.logger = logger;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            var account = new Account();

            return View("Register", account);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(Account account)
        {
            //var exists = db.Accounts.Count(a => a.UserName.Equals(account.UserName)) > 0;
            var exists = dbSQL.Accounts.Count(a => a.UserName.Equals(account.UserName)) > 0;

            if (!exists)
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
                account.Status = true;
                //db.Accounts.Add(account);
                //db.SaveChanges();
                dbSQL.AddAccount(account);

                var roleAccount = new RoleAccount()
                {
                    RoleId = 2,
                    AccountId = account.Id,
                    Status = true
                };
                //db.RoleAccounts.Add(roleAccount);
                //db.SaveChanges();
                dbSQL.AddRoleAccount(roleAccount);

                return RedirectToAction("Dashboard", "Customer");
            }
            else
            {
                ViewBag.error = "Username exists";
                account = new Account();
                return View("Register", account);
            }
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            var account = ProccesLogin(username, password);

            if (account != null)
            {
                securityManager.SignIn(this.HttpContext, account);
                //return RedirectToAction("index", "dashboard", new { area = "admin" });
                return RedirectToAction("dashboard", "customer");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Login");
            }
        }

        private Account ProccesLogin(string username, string password)
        {
            //var account = db.Accounts.SingleOrDefault(a => a.UserName.Equals(username));
            var account = dbSQL.Accounts.SingleOrDefault(a => a.UserName.Equals(username));

            if (account != null)
            {
                var roleOfAccount = account.RoleAccounts.FirstOrDefault();
                if (roleOfAccount.RoleId == 2 && roleOfAccount.Status && BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }

            return null;
        }

        [Route("signout")]
        public IActionResult SignOut()
        {
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("login", "customer");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            //var customer = db.Accounts.SingleOrDefault(a => a.UserName.Equals(user.Value));
            var customer = dbSQL.Accounts.SingleOrDefault(a => a.UserName.Equals(user.Value));

            return View("Profile", customer);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [Route("profile")]
        public IActionResult Profile(Account account)
        {
            //var currentCustomer = db.Accounts.Find(account.Id);
            var currentCustomer = dbSQL.Accounts.Find(a => a.Id == account.Id);

            if (!string.IsNullOrEmpty(account.Password))
            {
                currentCustomer.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            currentCustomer.FullName = account.FullName;
            currentCustomer.Email = account.Email;
            currentCustomer.Address = account.Address;
            currentCustomer.Phone = account.Phone;
            db.SaveChanges();

            return View("Profile", currentCustomer);
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult DashBoard()
        {
            return View("DashBoard");
        }

        [HttpGet]
        [Route("history")]
        public IActionResult History()
        {
            Claim user = User.FindFirst(ClaimTypes.Name);
            //Account customer = db.Accounts.SingleOrDefault(a => a.UserName.Equals(user.Value));
            Account customer = dbSQL.Accounts.SingleOrDefault(a => a.UserName.Equals(user.Value));

            ViewBag.invoices = customer.Invoices.OrderByDescending(i => i.Id).ToList();

            return View("History");
        }

        [HttpGet]
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            //ViewBag.invoicesDetails = db.InvoiceDetailses.Where(i => i.InvoiceId == id).ToList();
            ViewBag.invoicesDetails = dbSQL.InvoiceDetails.Where(i => i.InvoiceId == id).ToList();

            return View("Details");
        }
    }
} 
