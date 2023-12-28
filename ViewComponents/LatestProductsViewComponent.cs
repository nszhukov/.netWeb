using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ViewComponents
{
    [ViewComponent(Name = "LatestProducts")]
    public class LatestProductsViewComponent : ViewComponent
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;

        public LatestProductsViewComponent(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //List<Product> latestProducts = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status).Take(2).ToList();
            List<Product> latestProducts = dbSQL.Products.OrderByDescending(p => p.Id).Where(p => p.Status).Take(2).ToList();

            return View("Index", latestProducts);
        }
    }
}
