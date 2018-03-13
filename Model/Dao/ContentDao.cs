using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        BatmanShopDbContext db = null;

        public ContentDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get Content when having ID - Lấy ra Content khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        /// <summary>
        /// Insert one Content to database -  Thêm mới một Content vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(Content entity)
        {
            entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
            entity.Status = true;
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Update one Content in the database -  Cập nhật một Content trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                content.MetaTitle = entity.MetaTitle;
                content.Description = entity.Description;
                content.Image = entity.Image;
                content.ContentCategoryID = entity.ContentCategoryID;
                content.Detail = entity.Detail;
                content.Warranty = entity.Warranty;
                content.TopHot = entity.TopHot;
                content.Detail = entity.Detail;
                content.Tags = entity.Tags;
                content.MetaKeywords = entity.MetaKeywords;
                content.MetaDescriptions = entity.MetaDescriptions;
                content.ModifiedDate = DateTime.Now;
                content.Status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one Content in the database - Xóa một Content khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// List Content into a table with search string - Liệt kê danh sách Content có thể sử dụng tìm kiếm search
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// List all Contents
        /// </summary>
        /// <param name="productCategoryID"></param>
        /// <param name="totalRecord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Content> ListAll(ref int totalRecord, int pageIndex = 1, int pageSize = 9)
        {
            totalRecord = db.Contents.Count();
            var model = db.Contents.Where(x=>x.Status==true).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        /// <summary>
        /// List all Contents belong to the TagID
        /// </summary>
        /// <param name="totalRecord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Content> ListAllByTag(string tagId, ref int totalRecord, int pageIndex = 1, int pageSize = 9)
        {
            var model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContentID
                         where b.TagID == tagId
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             ID = a.ID

                         }).AsEnumerable().Select(x => new Content()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             ID = x.ID
                         });
            totalRecord = model.Count();
            model = model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        /// <summary>
        /// Get tag list of the content
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public List<Tag> ListTag(long contentId)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContentID == contentId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

        /// <summary>
        /// Get Tag when having TagId
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public Tag GetTag(string tagId)
        {
            return db.Tags.Find(tagId);
        }

        /// <summary>
        /// Get top new Contents
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Content> ListTopNewContent(int top)
        {
            return db.Contents.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }



    }
}
