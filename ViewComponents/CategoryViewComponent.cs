using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using MySqlConnector;
using System.Configuration;
using Microsoft.Data.Sqlite;
using Ecommerce.Services;

namespace Ecommerce.ViewComponents
{
    [ViewComponent(Name = "Category")]
    public class CategoryViewComponent : ViewComponent
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;

        public CategoryViewComponent(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = new List<Category>();

            //categories = db.Categories.Where(c => c.Status && c.ParrentId == null).ToList();
            categories = dbSQL.Categories.Where(c => c.Status && c.ParrentId == null).ToList();

            return View("Index", categories);
        }
    }
}
