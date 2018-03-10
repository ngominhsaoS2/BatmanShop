using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        BatmanShopDbContext db = null;

        public OrderDetailDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get OrderDetail when having orderID and productID - Lấy ra OrderDetail khi có orderID và productID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDetail GetByID(long orderID, long productID)
        {
            return db.OrderDetails.Single(x=>x.OrderID == orderID && x.ProductID == productID);
        }

        /// <summary>
        /// Insert one OrderDetail in the database -  Thêm mới một OrderDetail trong cơ sở dữ liệu
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Update one OrderDetail in the database -  Cập nhật một OrderDetail trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(OrderDetail entity)
        {
            try
            {
                var orderDetail = this.GetByID(entity.OrderID, entity.ProductID);
                orderDetail.Quantity = entity.Quantity;
                orderDetail.Price = entity.Price;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get list of OrderDetail when having OrderID - Lấy danh sách OrderDetail khi có OrderID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<OrderDetailViewModel> ListDetailByOrderId (long id)
        {
            var model = (from a in db.OrderDetails
                         join b in db.Products
                         on a.ProductID equals b.ID
                         where a.OrderID == id
                         select new
                         {
                             OrderID = a.OrderID,
                             ProductID = a.ProductID,
                             ProductCode = b.Code,
                             ProductName = b.Name,
                             Quantity = a.Quantity,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new OrderDetailViewModel()
                         {
                             OrderID = x.OrderID,
                             ProductID = x.ProductID,
                             ProductCode = x.ProductCode,
                             ProductName = x.ProductName,
                             Quantity = (int)x.Quantity,
                             Price = x.Price
                         });
            model = model.OrderByDescending(x => x.OrderID).OrderBy(x => x.ProductID);
            return model.ToList();
        }

        /// <summary>
        /// Delete one OrderDetail in the database - Xóa một OrderDetail khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long orderId, long productId)
        {
            try
            {
                var orderDetail = db.OrderDetails.SingleOrDefault(x=>x.OrderID == orderId && x.ProductID == productId);
                db.OrderDetails.Remove(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool CheckExistRow(long orderId, long productId)
        {
            var count = db.OrderDetails.Where(x => x.OrderID == orderId && x.ProductID == productId).Count();
            if(count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }




    }
}
