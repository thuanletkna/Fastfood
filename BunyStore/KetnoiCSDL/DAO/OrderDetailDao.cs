using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;

namespace KetnoiCSDL.DAO
{
    public class OrderDetailDao
    {
        BunyStoreDbContext db = null;
        public OrderDetailDao()
        {
            db = new BunyStoreDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
