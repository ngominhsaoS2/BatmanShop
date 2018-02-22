using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatmanShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(long productCategoryId, int pageIndex = 1, int pageSize = 9)
        {
            var productCategory = new ProductCategoryDao().GetByID(productCategoryId);
            ViewBag.ProductCategory = productCategory;

            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(productCategoryId, ref totalRecord, pageIndex, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = pageIndex;

            int maxPage = 5;
            int totalPage = 0;
            //totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            if(totalRecord % pageSize != 0)
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
            var product = new ProductDao().GetByID(id);
            ViewBag.ProductCategory = new ProductCategoryDao().GetByID((long)product.ProductCategoryID);
            ViewBag.NewRelatedProducts = new ProductDao().ListNewRelatedProduct(id, 3);
            ViewBag.HotRelatedProducts = new ProductDao().ListHotRelatedProduct(id, 3);
            return View(product);
        }






    }
}