using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserGroupDao
    {
        BatmanShopDbContext db = null;

        public UserGroupDao()
        {
            db = new BatmanShopDbContext();
        }

        public List<UserGroup> ListAll()
        {
            return db.UserGroups.ToList();
        }












    }
}
