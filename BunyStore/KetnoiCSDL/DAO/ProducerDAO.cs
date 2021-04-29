using KetnoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAdmin.DAO
{
    public class ProducerDAO
    {
        BunyStoreDbContext data = null;
        public ProducerDAO()
        {
            data = new BunyStoreDbContext();
        }

        public List<Producer> ListAll()
        {
            return data.Producers.ToList();
        }
        public Producer GetID(int id)
        {
            return data.Producers.Find(id);
        }

        public Producer GetName(string producername)
        {
            return data.Producers.Where(p => p.ProducerName == producername).SingleOrDefault();
        }

        public Producer GetAddress(string address)
        {
            return data.Producers.Where(p => p.Address == address).SingleOrDefault();
        }

    }
}
