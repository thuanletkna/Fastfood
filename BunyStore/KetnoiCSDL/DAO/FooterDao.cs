using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;

namespace KetnoiCSDL.DAO
{
    public class FooterDao
    {
        BunyStoreDbContext db = null;
        public FooterDao()
        {
            db = new BunyStoreDbContext();
        }
        public Footer LayFooter()
        {
            return db.Footers.SingleOrDefault();
        }
    }
}
