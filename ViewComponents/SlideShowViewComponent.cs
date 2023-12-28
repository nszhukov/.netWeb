using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Ecommerce.ViewComponents
{
    [ViewComponent(Name = "SlideShow")]
    public class SlideShowViewComponent : ViewComponent
    {
        private EcommerceContext db;
        private SQLiteConnectorService dbSQL;

        public SlideShowViewComponent(EcommerceContext db, SQLiteConnectorService dbSQL)
        {
            this.db = db;
            this.dbSQL = dbSQL;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //List<SlideShow> slideShows = db.SlideShows.Where(c => c.Status).ToList();
            List<SlideShow> slideShows = dbSQL.SlideShows.Where(c => c.Status).ToList();

            return View("Index", slideShows);
        }
    }
}
