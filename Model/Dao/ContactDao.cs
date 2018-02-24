using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        BatmanShopDbContext db = null;

        public ContactDao()
        {
            db = new BatmanShopDbContext();
        }

        public Contact GetActiveContent()
        {
            return db.Contacts.SingleOrDefault(x => x.Status == true);
        }







    }
}
