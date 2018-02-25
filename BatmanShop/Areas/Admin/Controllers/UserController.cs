using BatmanShop.Common;
using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        ////Display, create, edit, delete User
        //Index page of User management
        [HasPermission(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create a new User
        [HttpGet]
        public ActionResult Create()
        {
            SetGroupIDViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var encryptedMd5Pass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pass;

                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Create a new user successfully.", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Create a new user failed.");
                    return RedirectToAction("Create", "User");
                }
            }
            SetGroupIDViewBag();
            return View(user);

        }

        //Edit an User
        [HttpGet]
        public ActionResult Edit (long id)
        {
            var user = new UserDao().GetByID(id);
            PostGroupIDViewBag(user.GroupID);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit (User user)
        {
            if (ModelState.IsValid)
            {
                var encryptedMd5Pass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pass;

                var result = new UserDao().Update(user);
                if (result)
                {
                    SetAlert("Edit this user successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Edit this user failed.");    
                }

            }
            PostGroupIDViewBag(user.GroupID);
            return View(user);
        }

        //Delete an User
        [HttpDelete]
        public ActionResult Delete (long id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }








        ////Other methods
        //Set ViewBag for GroupID options
        public void SetGroupIDViewBag()
        {
            var dao = new UserGroupDao();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name");
        }

        public void PostGroupIDViewBag(string selectId)
        {
            var dao = new UserGroupDao();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name", selectId);
        }












    }
}