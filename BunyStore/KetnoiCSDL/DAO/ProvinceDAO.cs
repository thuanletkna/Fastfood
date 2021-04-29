using KetnoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetnoiCSDL.DAO
{
    public class ProvinceDAO
    {
        BunyStoreDbContext data = null;
        public ProvinceDAO()
        {
            data = new BunyStoreDbContext();
        }

        public Province GetProvince(int id)
        {
            return data.Provinces.Where(p => p.id == id).SingleOrDefault();
        }

        public Province GetNameProvince(string name)
        {
            return data.Provinces.Where(p => p.nameProvince == name).SingleOrDefault();
        }
    }
}
