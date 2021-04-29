using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;
using PagedList;
namespace KetnoiCSDL.DAO
{
    public class SlideDao
    {
        BunyStoreDbContext db = null;
        public SlideDao()
        {
            db = new BunyStoreDbContext();
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }

        public IEnumerable<Slide> ListAllPaging(string searchString, int pageNumber, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(pageNumber, pageSize);
        }
    }
}