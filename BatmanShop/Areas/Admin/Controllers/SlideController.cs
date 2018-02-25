using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        ////Display, create, edit, delete Slide
        //Index page of Slide management
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new SlideDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create a new Slide
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideDao();
                long id = dao.Insert(slide);
                if (id > 0)
                {
                    SetAlert("Create a new slide successfully.", "success");
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    ModelState.AddModelError("", "Create a new slide failed.");
                    return RedirectToAction("Create", "Slide");
                }
            }
            return View(slide);
        }

        //Edit an Slide
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var slide = new SlideDao().GetByID(id);
            return View(slide);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var result = new SlideDao().Update(slide);
                if (result)
                {
                    SetAlert("Edit this slide successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Edit this slide failed.");
                }

            }
            return View(slide);
        }

        //Delete an Slide
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new SlideDao().Delete(id);
            return RedirectToAction("Index");
        }












        ////Other methods









    }
}