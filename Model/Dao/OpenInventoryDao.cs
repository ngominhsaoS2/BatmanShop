using Model.EntityFramework;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OpenInventoryDao
    {
        BatmanShopDbContext db = null;

        public OpenInventoryDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get OpenInventory when having ID - Lấy ra OpenInventory khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OpenInventory GetByID(long id)
        {
            return db.OpenInventories.Find(id);
        }

        /// <summary>
        /// Insert one OpenInventory to database -  Thêm mới một OpenInventory vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(OpenInventory entity)
        {
            entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
            db.OpenInventories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Insert list of OpenInventory to database -  Thêm mới một list OpenInventory vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertList(List<OpenInventory> listOpenInventory)
        {
            try
            {
                foreach (var item in listOpenInventory)
                {
                    item.CreatedDate = item.ModifiedDate = DateTime.Now;
                    db.OpenInventories.Add(item);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex){
                return false;
            }
            
            
        }

        /// <summary>
        /// Update one OpenInventory in the database -  Cập nhật một OpenInventory trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(OpenInventory entity)
        {
            try
            {
                var openInventory = db.OpenInventories.Find(entity.ID);
                openInventory.WarehouseID = entity.WarehouseID;
                openInventory.Year = entity.Year;
                openInventory.Month = entity.Month;
                openInventory.ProductID = entity.ProductID;
                openInventory.Quantity = entity.Quantity;
                openInventory.ModifiedDate = DateTime.Now;
                openInventory.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one OpenInventory in the database - Xóa một OpenInventory khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var openInventory = db.OpenInventories.Find(id);
                db.OpenInventories.Remove(openInventory);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// List OpenInventory into a table with search string - Liệt kê danh sách OpenInventory có thể sử dụng tìm kiếm search
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<OpenInventoryViewModel> ListAllPaging(string searchString, int page, int pageSize, string month, int? warehouseId = null, int? year = null)
        {
            IQueryable<OpenInventoryViewModel> model = db.vOpenInventories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.WarehouseCode.Contains(searchString) || x.WarehouseName.Contains(searchString)
                || x.ProductCode.Contains(searchString) || x.ProductName.Contains(searchString));
            }
            if(year != null)
            {
                model = model.Where(x => x.Year == year);
            }
            if (!string.IsNullOrEmpty(month))
            {
                model = model.Where(x => x.Month.Contains(month));
            }
            if (warehouseId != null)
            {
                model = model.Where(x => x.WarehouseID == warehouseId);
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }



    }
}
