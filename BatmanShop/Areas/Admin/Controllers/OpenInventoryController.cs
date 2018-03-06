using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BatmanShop.Areas.Admin.Controllers
{
    public class OpenInventoryController : BaseController
    {
        ////Display, create, edit, delete User
        //Index page of OpenInventory management
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OpenInventoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create new list or OpenInventory
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(string listOpenInventory)
        {
            var jsonList = new JavaScriptSerializer().Deserialize<List<OpenInventory>>(listOpenInventory);

            foreach (var item in jsonList)
            {
                long id = new OpenInventoryDao().Insert(item);
                
            }

            return Json(new
            {
                status = true
            });
        }

        
















    }
}