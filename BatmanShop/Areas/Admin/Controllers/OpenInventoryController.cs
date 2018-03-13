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
        ////Display, create, edit, delete OpenInventory
        //Index page of OpenInventory management
        public ActionResult Index(string searchString, string month, int page = 1, int pageSize = 10, int? year = null, int? warehouseId = null )
        {
            var dao = new OpenInventoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize, month, warehouseId, year);
            ViewBag.SearchString = searchString;
            ViewBag.WarehouseID = warehouseId;
            ViewBag.Year = year;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create new list of OpenInventory
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

        //Edit an OpenInventory
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var openInventory = new OpenInventoryDao().GetByID(id);
            return View(openInventory);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(OpenInventory openInventory)
        {
            if (ModelState.IsValid)
            {
                var result = new OpenInventoryDao().Update(openInventory);
                if (result)
                {
                    SetAlert("Edit this open inventory successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Edit this open inventory failed.");
                }

            }
            return View(openInventory);
        }

        //Delete an OpenInventory
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new OpenInventoryDao().Delete(id);
            return RedirectToAction("Index");
        }



        ////Other methods








    }
}