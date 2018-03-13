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
    public class AccDocDao
    {
        BatmanShopDbContext db = null;

        public AccDocDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get AccDoc when having ID - Lấy ra AccDoc khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccDoc GetByID(long id)
        {
            return db.AccDocs.Find(id);
        }

        /// <summary>
        /// Insert one AccDoc to database -  Thêm mới một AccDoc vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(AccDoc entity)
        {
            entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
            entity.Status = true;
            db.AccDocs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public string UpdateDocNo(string docCode, long id)
        {
            var accDoc = db.AccDocs.Find(id);
            accDoc.DocNo = AccDocHelp.CreateDocNo(docCode, id);
            int l = accDoc.DocNo.Length;
            db.SaveChanges();
            return accDoc.DocNo;
        }

        /// <summary>
        /// Update one AccDoc in the database -  Cập nhật một AccDoc trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(AccDoc entity)
        {
            try
            {
                var accDoc = db.AccDocs.Find(entity.ID);
                accDoc.DocCode = entity.DocCode;
                accDoc.DocNo = entity.DocNo;
                accDoc.Date = entity.Date;
                accDoc.ModifiedDate = DateTime.Now;
                accDoc.Status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one AccDoc in the database - Xóa một AccDoc khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var accDoc = db.AccDocs.Find(id);
                db.AccDocs.Remove(accDoc);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// List AccDoc into a table with search string - Liệt kê danh sách AccDoc có thể sử dụng tìm kiếm search
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<AccDoc> ListAllPaging(string docCode, string searchString, int page, int pageSize)
        {
            IQueryable<AccDoc> model = db.AccDocs;
            model = model.Where(x => x.DocCode.Contains(docCode));
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.DocNo.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Check if the DocNo of a DocCode already exists or not
        /// </summary>
        /// <returns></returns>
        public bool CheckDocNo (string docCode, string docNo) {
            var result = db.AccDocs.Where(x => x.DocCode == docCode && x.DocNo == docNo);
            if (result.Count() > 0)
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
