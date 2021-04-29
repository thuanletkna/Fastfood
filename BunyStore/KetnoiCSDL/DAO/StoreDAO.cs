using KetnoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetnoiCSDL.DAO
{
    public class StorerDAO
    {
        BunyStoreDbContext data = null;
        public StorerDAO()
        {
            data = new BunyStoreDbContext();
        }

        public List<Store> ListAll()
        {
            return data.Stores.ToList();
        }
        public Store GetID(int id)
        {
            return data.Stores.Find(id);
        }

        public Store GetName(string nameStore)
        {
            return data.Stores.Where(p => p.nameStore == nameStore).SingleOrDefault();
        }

        public Store GetAddress(string address)
        {
            return data.Stores.Where(p => p.Address == address).SingleOrDefault();
        }

    }
}

