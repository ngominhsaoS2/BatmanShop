﻿using Model.EntityFramework;
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
                content.Status = entity.Status;
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





    }
}
