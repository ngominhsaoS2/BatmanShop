﻿using BatmanShop.Common;
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
    public class ProductController : BaseController
    {
        ////Display, create, edit, delete User
        //Index page of Product management
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create a new Product
        [HttpGet]
        public ActionResult Create()
        {
            SetProductCategoryIDViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create (Product product)
        {
            if (ModelState.IsValid)
            {
                //Xử lý MetaTitle
                if (string.IsNullOrEmpty(product.MetaTitle))
                {
                    product.MetaTitle = StringHelper.ToUnsignString(product.Name);
                }

                var dao = new ProductDao();
                long id = dao.Insert(product);
                if (id > 0)
                {
                    SetAlert("Create a new product successfully.", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Create a new product failed.");
                    return RedirectToAction("Create", "Product");
                }
            }
            SetProductCategoryIDViewBag();
            return View(product);
        }

        //Edit an Product
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var product = new ProductDao().GetByID(id);
            SetProductCategoryIDViewBag(product.ProductCategoryID);
            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.MetaTitle = StringHelper.ToUnsignString(product.Name);

                var result = new ProductDao().Update(product);
                if (result)
                {
                    SetAlert("Edit this product successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Edit this product failed.");
                }

            }
            SetProductCategoryIDViewBag(product.ProductCategoryID);
            return View(product);
        }


        //Delete an Product
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }









        ////Other methods
        //Set ViewBag for ProductCategoryID options
        public void SetProductCategoryIDViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.ProductCategoryID = new SelectList(dao.ListAll(), "ID", "Name");
        }






    }
}