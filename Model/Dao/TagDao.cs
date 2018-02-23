using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class TagDao
    {
        BatmanShopDbContext db = null;

        public TagDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get Tag when having ID - Lấy ra Tag khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tag GetByID(long id)
        {
            return db.Tags.Find(id);
        }

        /// <summary>
        /// Insert one Tag to database -  Thêm mới một Tag vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        /// <summary>
        /// Update one Tag in the database -  Cập nhật một Tag trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Tag entity)
        {
            try
            {
                var tag = db.Tags.Find(entity.ID);
                tag.ID = entity.ID;
                tag.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one Tag in the database - Xóa một Tag khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var tag = db.Tags.Find(id);
                db.Tags.Remove(tag);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if a tag already exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        /// <summary>
        /// List all tags of the content
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







    }
}
