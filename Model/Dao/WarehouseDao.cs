using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class WarehouseDao
    {
        BatmanShopDbContext db = null;

        public WarehouseDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get Warehouse when having ID - Lấy ra Warehouse khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Warehouse GetByID(long id)
        {
            return db.Warehouses.Find(id);
        }

        /// <summary>
        /// Insert one Warehouse to database -  Thêm mới một Warehouse vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(Warehouse entity)
        {
            entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
            db.Warehouses.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Update one Warehouse in the database -  Cập nhật một Warehouse trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Warehouse entity)
        {
            try
            {
                var warehouse = db.Warehouses.Find(entity.ID);
                warehouse.Code = entity.Code;
                warehouse.Name = entity.Name;
                warehouse.Address = entity.Address;
                warehouse.Phone = entity.Phone;
                warehouse.MetaTitle = entity.MetaTitle;
                warehouse.Description = entity.Description;
                warehouse.Image = entity.Image;
                warehouse.ModifiedDate = DateTime.Now;
                warehouse.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one Warehouse in the database - Xóa một Warehouse khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var warehouse = db.Warehouses.Find(id);
                db.Warehouses.Remove(warehouse);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// List Warehouse into a table with search string - Liệt kê danh sách Warehouse có thể sử dụng tìm kiếm search
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Warehouse> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Warehouse> model = db.Warehouses;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Code.Contains(searchString) || x.Name.Contains(searchString)
                || x.Address.Contains(searchString) || x.Phone.Contains(searchString) || x.Description.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get Warehouse list when having a keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<string> ListName(string keyword)
        {
            return db.Warehouses.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        //public List<Warehouse> ListName(string keyword)
        //{
        //    var listWarehouse = (from a in db.Warehouses
        //                         where a.Name.Contains(keyword)
        //                         select new
        //                         {
        //                             ID = a.ID,
        //                             Code = a.Code,
        //                             Name = a.Name,
        //                             Address = a.Address,
        //                             Phone = a.Phone,
        //                             MetaTitle = a.MetaTitle,
        //                             Description = a.Description,
        //                             Image = a.Image,
        //                             CreatedDate = a.CreatedDate,
        //                             CreatedBy = a.CreatedBy,
        //                             ModifiedDate = a.ModifiedDate,
        //                             ModifiedBy = a.ModifiedBy,
        //                             Status = a.Status
        //                         }).AsEnumerable().Select(x => new Warehouse()
        //                         {
        //                             ID = x.ID,
        //                             Code = x.Code,
        //                             Name = x.Name,
        //                             Address = x.Address,
        //                             Phone = x.Phone,
        //                             MetaTitle = x.MetaTitle,
        //                             Description = x.Description,
        //                             Image = x.Image,
        //                             CreatedDate = x.CreatedDate,
        //                             CreatedBy = x.CreatedBy,
        //                             ModifiedDate = x.ModifiedDate,
        //                             ModifiedBy = x.ModifiedBy,
        //                             Status = x.Status
        //                         });
        //    return listWarehouse.ToList();
        //}




    }
}
