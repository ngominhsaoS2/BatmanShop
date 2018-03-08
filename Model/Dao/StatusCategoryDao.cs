using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class StatusCategoryDao
    {
        BatmanShopDbContext db = null;

        public StatusCategoryDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get StatusCategory when having ID - Lấy ra StatusCategory khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StatusCategory GetByID(long id)
        {
            return db.StatusCategories.Find(id);
        }

        /// <summary>
        /// List all StatusCategory when having type - Lấy ra các StatusCategory khi có type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<StatusCategory> ListStatus(int type)
        {
            return db.StatusCategories.Where(x => x.Type == type).OrderBy(x => x.ID).ToList();
        }


    }
}
