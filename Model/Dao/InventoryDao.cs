using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class InventoryDao
    {
        BatmanShopDbContext db = null;

        public InventoryDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get Inventory when having ID - Lấy ra Inventory khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Inventory GetByID(long id)
        {
            return db.Inventories.Find(id);
        }

        /// <summary>
        /// Get Inventory when having ActionID, OrderID, ProductID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Inventory GetBy(long actionID, long orderID, long productID)
        {
            return db.Inventories.SingleOrDefault(x => x.ActionID == actionID && x.OrderID == orderID && x.ProductID == productID);
        }

        public List<Inventory> ListInventoryByOrderId(long orderId)
        {
            return db.Inventories.Where(x => x.OrderID == orderId).ToList();
        }

        public List<InventoryViewModel> ListInventoryViewByOrderId(long orderId)
        {
            return db.vInventories.Where(x => x.OrderID == orderId).ToList();
        }

        /// <summary>
        /// Insert one Inventory to database -  Thêm mới một Inventory vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(Inventory entity)
        {
            entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
            db.Inventories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Update one Inventory in the database -  Cập nhật một Inventory trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Inventory entity)
        {
            try
            {
                var inventory = db.Inventories.Find(entity.ID);
                inventory.ActionID = entity.ActionID;
                inventory.Date = entity.Date;
                inventory.OrderID = entity.OrderID;
                inventory.WarehouseID = entity.WarehouseID;
                inventory.ProductID = entity.ProductID;
                inventory.Quantity = entity.Quantity;
                inventory.ModifiedDate = DateTime.Now;
                inventory.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }











    }
}
