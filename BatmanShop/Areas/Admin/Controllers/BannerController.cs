using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Areas.Admin.Controllers
{
    public class BannerController : BaseController
    {
        ///Display, create, edit, delete Slide
        //Index page of Banner management
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BannerDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create a new Banner
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Banner banner)
        {
            if (ModelState.IsValid)
            {
                var dao = new BannerDao();
                long id = dao.Insert(banner);
                if (id > 0)
                {
                    SetAlert("Create a new banner successfully.", "success");
                    return RedirectToAction("Index", "banner");
                }
                else
                {
                    ModelState.AddModelError("", "Create a new banner failed.");
                    return RedirectToAction("Create", "Banner");
                }
            }
            return View(banner);
        }

        //Edit an Slide
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var banner = new BannerDao().GetByID(id);
            return View(banner);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                var result = new BannerDao().Update(banner);
                if (result)
                {
                    SetAlert("Edit this banner successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "banner this banner failed.");
                }

            }
            return View(banner);
        }

        //Delete an Banner
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new BannerDao().Delete(id);
            return RedirectToAction("Index");
        }























    }
}