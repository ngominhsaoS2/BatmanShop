using BatmanShop.Models;
using Common;
using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BatmanShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddNew(long productId, int quantity)
        {
            var product = new ProductDao().GetByID(productId);
            var cart = Session[CartSession];
            if(cart != null)
            {
                //Kiểm tra xem product này đã tồn tại trong cart chưa
                var list = (List<CartItem>)Session[CartSession];
                var itemExist = list.SingleOrDefault(x => x.Product.ID == productId);
                if(itemExist != null)
                {
                    itemExist.Quantity = itemExist.Quantity + quantity;
                    list.RemoveAt(list.IndexOf(itemExist));
                    list.Add(itemExist);
                }
                else
                {
                    //Khởi tạo một cái CartItem mới
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    //Thêm item vào giỏ hàng
                    list.Add(item);
                }
                //Gán vào Cart session
                Session[CartSession] = list;
            }
            else
            {
                //Khởi tạo một cái CartItem mới
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                //Khởi tạo một cái giỏ hàng, giỏ hàng này có dạng là một List<CartItem>; sau đó thêm item vào giỏ hàng
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào Cart session
                Session[CartSession] = list;
            }

            return RedirectToAction("Index");
        }

        //Đoạn này copy nguyên từ bản master
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        //Đoạn này cũng copy nguyên từ bản master
        //2018.03.07 giờ thì hiểu rồi nhé =)) ahihi
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        //Đoạn này cũng copy nguyên từ bản master
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Pay()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Pay(string shipName, string shipMobile, string shipAddress, string shipEmail)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipMobile = shipMobile;
            order.ShipAddress = shipAddress;
            order.ShipEmail = shipEmail;
            order.Status = 2;
            try
            {
                var dao = new OrderDao();
                var id = dao.Insert(order);
                order.DocNo = dao.UpdateDocNo("OR", id);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);
                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", shipMobile);
                content = content.Replace("{{Email}}", shipEmail);
                content = content.Replace("{{Address}}", shipAddress);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(shipEmail, "New order from BatmanShop", content);
                new MailHelper().SendMail(toEmail, "New order from BatmanShop", content);

            }
            catch (Exception ex)
            {
                return Redirect("/Unsuccessful");
            }

            return Redirect("/Successful");
        }

        public ActionResult Success()
        {
            Session[BatmanShop.Common.CommonConstants.CartSession] = null;
            return View();
        }



    }
}