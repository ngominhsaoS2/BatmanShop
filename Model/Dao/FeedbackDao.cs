using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedbackDao
    {
        BatmanShopDbContext db = null;

        public FeedbackDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get Feedback when having ID - Lấy ra Feedback khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Feedback GetByID(long id)
        {
            return db.Feedbacks.Find(id);
        }

        /// <summary>
        /// Insert one Feedback to database -  Thêm mới một Feedback vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(Feedback entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            db.Feedbacks.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Delete one Feedback in the database - Xóa một Feedback khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var feedback = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// List Feedback into a table with search string - Liệt kê danh sách Feedback có thể sử dụng tìm kiếm search
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Feedback> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedbacks;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Phone.Contains(searchString)
                || x.Email.Contains(searchString) || x.Address.Contains(searchString) || x.Content.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }







    }
}
