using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            ViewBag.ProductCategory = new ProductCategoryDao().ListAll();
            ViewBag.MenuType2 = new MenuDao().ListByTypeId(2);
            var model = new MenuDao().ListByTypeId(1);
            return PartialView(model);
        }









    }
}