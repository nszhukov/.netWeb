using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Security;
using System.Security.Claims;
using Ecommerce.Services;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/login")]
    public class LoginController : Controller
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;
        private SecurityManager securityManager = new SecurityManager();

        public LoginController(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(string username, string password)
        {
            var account = ProccesLogin(username, password);

            if (account != null)
            {
                securityManager.SignIn(this.HttpContext, account);
                //return RedirectToAction("index", "dashboard", new { area = "admin" });
                return RedirectToAction("index", "dashboard", new { area = "admin" });
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }

        private Account ProccesLogin(string username, string password)
        {
            var account = dbSQL.Accounts.SingleOrDefault(a => a.UserName.Equals(username));

            if (account != null)
            {
                var roleOfAccount = account.RoleAccounts.FirstOrDefault();
                if (roleOfAccount.RoleId == 1 && roleOfAccount.Status  && BCrypt.Net.BCrypt.Verify(password, account.Password))
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
            return RedirectToAction("index", "login", new { area = "admin" });
        }

        [HttpGet] 
        [Route("profile")]
        public IActionResult Profile()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var username = user.Value;
            var account = dbSQL.Accounts.Single(a => a.UserName.Equals(username));

            return View("Profile", account);
        }

        [HttpPost]
        [Route("profile")]
        public IActionResult Profile(Account account)
        {
            var currentAccount = dbSQL.Accounts.Single(a => a.Id == account.Id);
            if (!string.IsNullOrEmpty(account.Password))
            {
                currentAccount.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            currentAccount.UserName = account.UserName;
            currentAccount.Email = account.Email;
            currentAccount.FullName = account.FullName;
            currentAccount.Address = account.Address;
            currentAccount.Phone = account.Phone;
            db.SaveChanges();


            ViewBag.msg = "Done";

            return View("Profile");
        }

        [Route("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
