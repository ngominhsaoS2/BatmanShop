using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentTagDao
    {
        BatmanShopDbContext db = null;

        public ContentTagDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get ContentTag when having contentID and tagID - Lấy ra ContentTag khi có contentID và tagID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContentTag GetByID(long contentID, string tagID)
        {
            return db.ContentTags.Single(x => x.ContentID == contentID && x.TagID == tagID);
        }

        /// <summary>
        /// Insert ContentTag
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="tagId"></param>
        public void Insert(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }

        /// <summary>
        ///Remove all content tags of the contentId 
        /// </summary>
        /// <param name="contentId"></param>
        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
            db.SaveChanges();
        }







    }
}
