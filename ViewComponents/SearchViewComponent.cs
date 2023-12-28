using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ViewComponents
{
    [ViewComponent(Name = "Search")]
    public class SearchViewComponent : ViewComponent
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;

        public SearchViewComponent(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string keyword = HttpContext.Request.Query["keyword"].ToString();
            /*int categoryId = -1;

            if (HttpContext.Request.Query.ContainsKey("categoryId"))
            {
                categoryId = int.Parse(HttpContext.Request.Query["categoryId"].ToString());
            }

            List<Category> categories = db.Categories.Where(c => c.Status && c.Parent == null).ToList();*/

            return View("Index", new InvokeResult() { keyword = keyword });
        }
    }

    public class InvokeResult
    {
        public string keyword { get; set; }
        //public int categoryId { get; set; }
        //public List<Category> categories { get; set; }
    }
}
