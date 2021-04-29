using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;
using KetnoiCSDL.DAO;

namespace KetnoiCSDL.DAO
{
    public class ContactDao
    {
        BunyStoreDbContext db = null;
        public ContactDao()
        {
            db = new BunyStoreDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
