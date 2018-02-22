using BatmanShop.Areas.Admin.Models;
using BatmanShop.Common;
using BatmantShop;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login (LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                if (result == 1)
                {
                    var user = dao.GetByUserName(model.UserName);

                    //Phần này là xử lý cho session (chưa cần hiểu lắm)
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;
                    userSession.Image = user.Image;

                    //Add login session
                    Session.Add(CommonConstants.USER_SESSION, userSession);

                    //Cuối cùng là trả về trang home: Index là action, Home là controller
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "The account does not exist");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "The account is locked");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Incorrect password");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Your account is not allow to access");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login");
                }
            }

            return View("Index");
        }





    }
}