using BatmantShop.Common;
using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Areas.Admin.Controllers
{
    public class WarehouseController : BaseController
    {
        ////Display, create, edit, delete Warehouse
        //Index page of Warehouse management
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new WarehouseDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create a new Warehouse
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                //Xử lý MetaTitle
                if (string.IsNullOrEmpty(warehouse.MetaTitle))
                {
                    warehouse.MetaTitle = StringHelper.ToUnsignString(warehouse.Name);
                }

                var dao = new WarehouseDao();
                long id = dao.Insert(warehouse);
                if (id > 0)
                {
                    SetAlert("Create a new warehouse successfully.", "success");
                    return RedirectToAction("Index", "Warehouse");
                }
                else
                {
                    ModelState.AddModelError("", "Create a new warehouse failed.");
                    return RedirectToAction("Create", "Warehouse");
                }
            }
            return View(warehouse);
        }

        //Edit an Warehouse
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var warehouse = new WarehouseDao().GetByID(id);
            return View(warehouse);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                warehouse.MetaTitle = StringHelper.ToUnsignString(warehouse.Name);

                var result = new WarehouseDao().Update(warehouse);
                if (result)
                {
                    SetAlert("Edit this warehouse successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Edit this warehouse failed.");
                }

            }
            return View(warehouse);
        }


        //Delete an Warehouse
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new WarehouseDao().Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult ListName(string q)
        {
            var data = new WarehouseDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }








    }
}