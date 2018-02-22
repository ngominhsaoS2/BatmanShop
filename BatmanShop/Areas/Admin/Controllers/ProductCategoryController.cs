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
    public class ProductCategoryController : BaseController
    {
        ////Display, create, edit, delete User
        //Index page of ProductCategory management
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create a new ProductCategory
        [HttpGet]
        public ActionResult Create()
        {
            SetParentIDViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                //Xử lý MetaTitle
                if (string.IsNullOrEmpty(productCategory.MetaTitle))
                {
                    productCategory.MetaTitle = StringHelper.ToUnsignString(productCategory.Name);
                }

                var dao = new ProductCategoryDao();
                long id = dao.Insert(productCategory);
                if (id > 0)
                {
                    SetAlert("Create a new product category successfully.", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Create a new product category failed.");
                    return RedirectToAction("Create", "ProductCategory");
                }
            }
            SetParentIDViewBag();
            return View(productCategory);
        }

        //Edit an ProductCategory
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var productCategory = new ProductCategoryDao().GetByID(id);
            SetParentIDViewBag(productCategory.ParentID);
            return View(productCategory);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                productCategory.MetaTitle = StringHelper.ToUnsignString(productCategory.Name);

                var result = new ProductCategoryDao().Update(productCategory);
                if (result)
                {
                    SetAlert("Edit this product category successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Edit this product category failed.");
                }

            }
            SetParentIDViewBag(productCategory.ParentID);
            return View(productCategory);
        }


        //Delete an ProductCategory
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }














        ////Other methods
        //Set ViewBag for ParentID options
        public void SetParentIDViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.ParentID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);    
        }





    }
}