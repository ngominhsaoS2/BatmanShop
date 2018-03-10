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
        [HttpGet]
        public ActionResult Supply(long id)
        {
            var order = new OrderDao().GetByID(id);
            var inventory = new InventoryDao().ListInventoryViewByOrderId(id);
            ViewBag.Inventory = inventory;
            SetStatusViewBag();
            return View(order);
        }

        [HttpPost]
        public ActionResult Supply(Order order)
        {
            if (ModelState.IsValid)
            {
                //Đổi trạng thái của Order về 3: Supplied
                order.Status = new OrderDao().ChangeStatus(order.ID, 3);
                var detailList = new OrderDetailDao().ListDetailByOrderId(order.ID);
                var invent = new Inventory();
                var dao = new InventoryDao();

                if(dao.ListInventoryByOrderId(order.ID).Count() == 0)
                {
                    foreach (var item in detailList)
                    {
                        invent.ActionID = 2;
                        invent.Date = invent.CreatedDate = invent.ModifiedDate = DateTime.Now;
                        invent.OrderID = item.OrderID;
                        invent.ProductID = item.ProductID;
                        invent.Quantity = item.Quantity;
                        invent.Status = true;
                        var id = dao.Insert(invent);
                    }
                }
                

                if (order.Status == 3)
                {
                    SetAlert("Supply this order successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Supply this order failed.");
                }

            }
            var inventory = new InventoryDao().ListInventoryViewByOrderId(order.ID);
            ViewBag.Inventory = inventory;
            SetStatusViewBag(order.Status);
            return View(order);
        }

        public JsonResult UpdateQuantity(string cartModel, long orderId)
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

        public JsonResult UpdateWarehouseID(string list)
        {
            var json = new JavaScriptSerializer().Deserialize<List<Inventory>>(list);
            var listInventory = new InventoryDao().ListInventoryByOrderId(json[0].OrderID);
            var dao = new InventoryDao();
            Inventory inventory = new Inventory();

            if (listInventory.Count() > 0)
            {
                foreach (var item in json)
                {
                    var rem = listInventory.Single(x => x.ProductID == item.ProductID);
                    item.ID = rem.ID;
                    listInventory.Remove(rem);
                    listInventory.Add(item);
                }

                foreach (var item in listInventory)
                {
                    inventory.ID = item.ID;
                    inventory.OrderID = item.OrderID;
                    inventory.ProductID = item.ProductID;
                    inventory.Quantity = item.Quantity;
                    inventory.WarehouseID = item.WarehouseID;
                    dao.Update(inventory);
                }

            }
            else
            {
                foreach (var item in json)
                {
                    listInventory.Add(item);
                }

                foreach (var item in listInventory)
                {
                    dao.Insert(item);
                }
            }

            return Json(new
            {
                status = true
            });
        }

        public JsonResult AddRow(string row)
        {
            var json = new JavaScriptSerializer().Deserialize<OrderDetail>(row);
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderID = json.OrderID;
            orderDetail.ProductID = json.ProductID;
            orderDetail.Price = json.Price;
            orderDetail.Quantity = json.Quantity;
            var dao = new OrderDetailDao();
            if(dao.CheckExistRow(orderDetail.OrderID, orderDetail.ProductID) == false)
            {
                dao.Insert(orderDetail);
                return Json(new
                {
                    status = true
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