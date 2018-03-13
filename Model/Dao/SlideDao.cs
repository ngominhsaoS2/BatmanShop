using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SlideDao
    {
        BatmanShopDbContext db = null;

        public SlideDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get Slide when having ID - Lấy ra Slide khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Slide GetByID(long id)
        {
            return db.Slides.Find(id);
        }

        /// <summary>
        /// Insert one Slide to database -  Thêm mới một Slide vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(Slide entity)
        {
            entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
            entity.Status = true;
            db.Slides.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Update one Slide in the database -  Cập nhật một Slide trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Slide entity)
        {
            try
            {
                var slide = db.Slides.Find(entity.ID);
                slide.Name = entity.Name;
                slide.Image = entity.Image;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.Link = entity.Link;
                slide.Description = entity.Description;
                slide.ModifiedDate = DateTime.Now;
                slide.Status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one Slide in the database - Xóa một Slide khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// List Slide into a table with search string - Liệt kê danh sách Slide có thể sử dụng tìm kiếm search
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }


        /// <summary>
        /// Get top new Slides
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Slide> ListTopNewSlide(int top)
        {
            return db.Slides.Where(x=>x.Status==true).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }









    }
}
