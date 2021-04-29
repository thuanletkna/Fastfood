using KetnoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetnoiCSDL.DAO
{
    public class DiscountDAO
    {
        BunyStoreDbContext data = null;
        //Tạo constructor
        public DiscountDAO()
        {
            data = new BunyStoreDbContext();
        }

        public List<Content> ListAll()
        {
            return data.Contents.ToList();
        }

        public Content GetDisCount(string meta)
        {
            try
            {
                return data.Contents.Where(p => p.MetaTitle == meta).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
