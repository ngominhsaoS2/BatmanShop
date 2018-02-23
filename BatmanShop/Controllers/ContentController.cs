using BatmantShop.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(int pageIndex = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new ContentDao().ListAll(ref totalRecord, pageIndex, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = pageIndex;

            int maxPage = 5;
            int totalPage = 0;
            if (totalRecord % pageSize != 0)
            {
                totalPage = (int)(totalRecord / pageSize) + 1;
            }
            else
            {
                totalPage = (int)(totalRecord / pageSize);
            }

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = pageIndex + 1;
            ViewBag.Prev = pageIndex - 1;

            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var content = new ContentDao().GetByID(id);
            //Lấy các Tags của content
            ViewBag.Tags = new ContentDao().ListTag(id);
            //Lấy Tag đầu tiên của content
            string[] tags = content.Tags.Split(',');
            string firstTag = StringHelper.ToUnsignString(tags[0]);
            //Lấy các tin liên quan của Tag đầu tiên vừa tìm
            int top = 5;
            int totalRecord = 0;
            ViewBag.RelatedContent = new ContentDao().ListAllByTag(firstTag, ref totalRecord, 1, top);
            return View(content);
        }

        public ActionResult Tag(string tagId, int pageIndex = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var model = new ContentDao().ListAllByTag(tagId, ref totalRecord, pageIndex, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = pageIndex;

            int maxPage = 5;
            int totalPage = 0;
            if (totalRecord % pageSize != 0)
            {
                totalPage = (int)(totalRecord / pageSize) + 1;
            }
            else
            {
                totalPage = (int)(totalRecord / pageSize);
            }

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = pageIndex + 1;
            ViewBag.Prev = pageIndex - 1;

            ViewBag.TagId = tagId;

            return View(model);
        }






    }
}