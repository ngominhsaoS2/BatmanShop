using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContent();
            return View(model);
        }

        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Phone = mobile;
            feedback.Address = address;
            feedback.Email = email;
            feedback.Content = content;
            feedback.CreatedDate = DateTime.Now;

            var id = new FeedbackDao().Insert(feedback);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                    //Có thể send mail ở đây, làm tương tự như bài gửi mail
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }

        }



    }
}