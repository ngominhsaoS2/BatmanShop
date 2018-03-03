using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Create(List<OpenInventory> listOpenInventory)
        {
            if (ModelState.IsValid)
            {
                var dao = new OpenInventoryDao();
                bool id = dao.InsertList(listOpenInventory);
                if (id)
                {
                    SetAlert("Create successfully.", "success");
                    return RedirectToAction("Index", "OpenInventory");
                }
                else
                {
                    ModelState.AddModelError("", "Create failed.");
                    return RedirectToAction("Create", "OpenInventory");
                }
            }
            return View(listOpenInventory);
        }

        
















    }
}