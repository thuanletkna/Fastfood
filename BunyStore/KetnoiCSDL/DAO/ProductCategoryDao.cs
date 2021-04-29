using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;
using PagedList;

namespace KetnoiCSDL.DAO
{
    public class ProductCategoryDao
    {
        BunyStoreDbContext db = null;
        public ProductCategoryDao()
        {
            db = new BunyStoreDbContext();
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public List<ProductCategory> Combonhom(int top)
        {
            List<ProductCategory> combonhom = (from data in db.ProductCategories
                                       where data.ID == 13
                                       select data).Take(top).ToList();

            return combonhom;
        }
        public List<ProductCategory> Combonhom1(int top)
        {
            List<ProductCategory> combonhom = (from data in db.ProductCategories
                                               where data.ID == 15
                                               select data).Take(top).ToList();

            return combonhom;
        }
        public List<ProductCategory> Combonhom2(int top)
        {
            List<ProductCategory> combonhom = (from data in db.ProductCategories
                                               where data.ID == 17
                                               select data).Take(top).ToList();

            return combonhom;
        }
        public List<ProductCategory> ListNewProductcategory(int top)
        {
            return db.ProductCategories.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public ProductCategory GetID(int id)
        {
            return db.ProductCategories.Find(id);
        }

        public ProductCategory GetName(string nametype)
        {
            return db.ProductCategories.Where(p => p.Name == nametype).SingleOrDefault();
        }
        public List<ProductCategory> GetIMG(string img)
        {
            return db.ProductCategories.Where(p => p.Image == img).ToList();
        }
        public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}