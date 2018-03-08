using BatmanShop.Models;
using Model.Dao;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BatmanShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        ////Display, create, edit, delete Order
        //Index page of Order management
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Edit an Order
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var order = new OrderDao().GetByID(id);
            var orderDetail = new OrderDetailDao().ListDetailByOrderId(id);
            ViewBag.OrderDetail = orderDetail;
            SetStatusViewBag();
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                var result = new OrderDao().Update(order);
                if (result)
                {
                    SetAlert("Edit this order successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Edit this order failed.");
                }

            }
            var orderDetail = new OrderDetailDao().ListDetailByOrderId(order.ID);
            ViewBag.OrderDetail = orderDetail;
            SetStatusViewBag(order.Status);
            return View(order);
        }

        //Suplly an Order


        public JsonResult UpdateRow(string cartModel, long orderId)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var listCartItem = (List<CartItem>)this.ListCartItemByOrderId(orderId);

            foreach (var item in listCartItem)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            var detailDao = new OrderDetailDao();
            OrderDetail orderDetail = new OrderDetail();

            foreach (var item in listCartItem)
            {
                orderDetail.OrderID = orderId;
                orderDetail.ProductID = item.Product.ID;
                orderDetail.Quantity = item.Quantity;
                orderDetail.Price = item.Product.Price;
                detailDao.Update(orderDetail);
            }

            return Json(new
            {
                status = true
            });
        }

        public List<CartItem> ListCartItemByOrderId(long id)
        {
            var dao = new OrderDetailDao();
            List<OrderDetailViewModel> listOrderDetail = dao.ListDetailByOrderId(id);
            List<CartItem> listCartItem = new List<CartItem>();
            Product product = new Product();
            CartItem cartItem = new CartItem();

            foreach (var item in listOrderDetail)
            {
                product = new ProductDao().GetByID(item.ProductID);
                cartItem = new CartItem() { Product = product, Quantity = item.Quantity};
                listCartItem.Add(cartItem);
            }

            return listCartItem;

        }

        //Delete an OrderDetail
        [HttpDelete]
        public ActionResult Delete(long orderId, long productId)
        {
            new OrderDetailDao().Delete(orderId, productId);
            return RedirectToAction("#");
        }

        ////Other methods
        //Set ViewBag for Status options
        public void SetStatusViewBag(long? selectedId = null)
        {
            var dao = new StatusCategoryDao();
            ViewBag.Status = new SelectList(dao.ListStatus(1), "ID", "Name");
        }





    }
}