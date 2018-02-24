using Model.EntityFramework;
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

        public long Insert(Feedback feedback)
        {
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            return feedback.ID;
        }






    }
}
