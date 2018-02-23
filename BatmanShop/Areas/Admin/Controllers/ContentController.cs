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
    public class ContentController : BaseController
    {
        ////Display, create, edit, delete User
        //Index page of Content management
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Create a new Content
        [HttpGet]
        public ActionResult Create()
        {
            SetContentCategoryIDViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content content)
        {
            if (ModelState.IsValid)
            {
                //Xử lý MetaTitle, sau đó insert content đã được xử lý vào database
                if (string.IsNullOrEmpty(content.MetaTitle))
                {
                    content.MetaTitle = StringHelper.ToUnsignString(content.Name);
                }

                var dao = new ContentDao();
                long id = dao.Insert(content);
                
                //Xử lý bảng Tag, ContentTag
                if (!string.IsNullOrEmpty(content.Tags))
                {
                    string[] tags = content.Tags.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.ToUnsignString(tag);
                        var tagDao = new TagDao();
                        var contentTagDao = new ContentTagDao();
                        var existedTag = tagDao.CheckTag(tagId);

                        //insert vào bảng Tag trong database
                        if (!existedTag)
                        {
                            tagDao.Insert(tagId, tag);
                        }

                        //insert vào bảng ContentTag
                        contentTagDao.Insert(content.ID, tagId);
                    }
                }

                if (id > 0)
                {
                    SetAlert("Create a new content successfully.", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Create a new content failed.");
                    return RedirectToAction("Create", "Content");
                }
            }
            SetContentCategoryIDViewBag();
            return View(content);
        }

        //Edit an Product
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var content = new ContentDao().GetByID(id);
            SetContentCategoryIDViewBag(content.ContentCategoryID);
            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                //Xử lý MetaTitle, sau đó update content đã được xử lý vào database
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
                var result = new ContentDao().Update(content);

                //Xử lý bảng Tag, ContentTag
                var contentTagDao = new ContentTagDao();
                contentTagDao.RemoveAllContentTag(content.ID);

                if (!string.IsNullOrEmpty(content.Tags))
                {
                    string[] tags = content.Tags.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.ToUnsignString(tag);
                        var tagDao = new TagDao();
                        var existedTag = tagDao.CheckTag(tagId);

                        //insert vào bảng Tag trong database
                        if (!existedTag)
                        {
                            tagDao.Insert(tagId, tag);
                        }

                        //insert vào bảng ContentTag
                        contentTagDao.Insert(content.ID, tagId);
                    }
                }

                if (result)
                {
                    SetAlert("Edit this content successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Edit this content failed.");
                }

            }
            SetContentCategoryIDViewBag(content.ContentCategoryID);
            return View(content);
        }

        //Delete an Product
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ContentDao().Delete(id);
            return RedirectToAction("Index");
        }





        ////Other methods
        //Set ViewBag for ContentCategoryID options
        public void SetContentCategoryIDViewBag(long? selectedId = null)
        {
            var dao = new ContentCategoryDao();
            ViewBag.ContentCategoryID = new SelectList(dao.ListAll(), "ID", "Name");
        }






    }
}