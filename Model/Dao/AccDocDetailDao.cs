using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AccDocDetailDao
    {
        BatmanShopDbContext db = null;

        public AccDocDetailDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get AccDocDetail when having ID - Lấy ra AccDocDetail khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccDocDetail GetByID(long id)
        {
            return db.AccDocDetails.Find(id);
        }

        /// <summary>
        /// Insert one AccDocDetail to database -  Thêm mới một AccDocDetail vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(AccDocDetail entity)
        {
            entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
            entity.Status = true;
            db.AccDocDetails.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Update one AccDocDetail in the database -  Cập nhật một AccDocDetail trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(AccDocDetail entity)
        {
            try
            {
                var accDocDetail = db.AccDocDetails.Find(entity.ID);
                accDocDetail.WarehouseID = entity.WarehouseID;
                accDocDetail.ProductID = entity.ProductID;
                accDocDetail.Price = entity.Price;
                accDocDetail.Quantity = entity.Quantity;
                accDocDetail.ModifiedDate = DateTime.Now;
                accDocDetail.Status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one AccDocDetail in the database - Xóa một AccDocDetail khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var accDocDetail = db.AccDocDetails.Find(id);
                db.AccDocDetails.Remove(accDocDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Check a detail row already exists or not
        /// </summary>
        /// <param name="docCode"></param>
        /// <param name="docNo"></param>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool CheckExist(string docCode, string docNo, long warehouseId, long productId)
        {
            var detail = db.AccDocDetails.Count(x => x.DocCode == docCode && x.DocNo == docNo && x.WarehouseID == warehouseId && x.ProductID == productId);
            if(detail > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update quantity of a detail row
        /// </summary>
        /// <param name="docCode"></param>
        /// <param name="docNo"></param>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        public void UpdateQuantity(string docCode, string docNo, long warehouseId, long productId, int quantity)
        {
            var detail = db.AccDocDetails.SingleOrDefault(x => x.DocCode == docCode && x.DocNo == docNo && x.WarehouseID == warehouseId && x.ProductID == productId);
            detail.Quantity = quantity;
            db.SaveChanges();
        }

        /// <summary>
        /// List all detail when having docCode and docNo
        /// </summary>
        /// <param name="docCode"></param>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public List<AccDocDetail> ListDetail(string docCode, string docNo)
        {
            return db.AccDocDetails.Where(x => x.DocCode == docCode && x.DocNo == docNo).ToList();
        }

        public void RemoveDetail(string docNo)
        {
            var list = db.AccDocDetails.Where(x => x.DocNo.Contains(docNo)).ToList();
            db.AccDocDetails.RemoveRange(list);
            db.SaveChanges();
        }



    }
}
