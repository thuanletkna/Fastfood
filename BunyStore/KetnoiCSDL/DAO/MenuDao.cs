using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;
namespace KetnoiCSDL.DAO
{
    public class MenuDao
    {
        BunyStoreDbContext db = null;
        public MenuDao()
        {
            db = new BunyStoreDbContext();
        }
        public List<Menu> ListByGroupID(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId).ToList();
        }
    }
}