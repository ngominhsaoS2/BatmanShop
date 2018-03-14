using Common;
using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
        BatmanShopDbContext db = null;

        public OrderDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get Order when having ID - Lấy ra Order khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetByID(long id)
        {
            return db.Orders.Find(id);
        }

        /// <summary>
        /// Insert one Order to database -  Thêm mới một Order vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(Order entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Update one Order in the database -  Cập nhật một Order trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Order entity)
        {
            try
            {
                var order = db.Orders.Find(entity.ID);
                order.CustomerID = entity.CustomerID;
                order.ShipName = entity.ShipName;
                order.ShipMobile = entity.ShipMobile;
                order.ShipAddress = entity.ShipAddress;
                order.ShipEmail = entity.ShipEmail;
                order.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one Order in the database - Xóa một Order khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var order = db.Orders.Find(id);
                db.Orders.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int ChangeStatus(long orderId, int status)
        {
            try
            {
                var order = db.Orders.Find(orderId);
                order.Status = status;
                db.SaveChanges();
                return status;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// List Order into a table with search string - Liệt kê danh sách Order có thể sử dụng tìm kiếm search
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.CustomerID.ToString().Contains(searchString) || x.ShipName.Contains(searchString)
                || x.ShipMobile.Contains(searchString) || x.ShipAddress.Contains(searchString) || x.ShipEmail.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public string UpdateDocNo(string docCode, long id)
        {
            var order = db.Orders.Find(id);
            order.DocNo = AccDocHelp.CreateDocNo(docCode, id);
            db.SaveChanges();
            return order.DocNo;
        }



    }
}
