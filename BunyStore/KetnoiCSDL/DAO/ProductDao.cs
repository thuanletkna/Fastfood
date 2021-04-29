using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KetnoiCSDL.EF;
using PagedList;

namespace KetnoiCSDL.DAO
{
    public class ProductDao
    {
        BunyStoreDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public ProductDao()
        {
            db = new BunyStoreDbContext();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Product> ListByCategoryId(long categoryID)
        {
            return db.Products.Where(x => x.CategoryID == categoryID).ToList();
        }

        public List<Product> ListAll()
        {
            return db.Products.ToList();
        }

        public List<Product> ListFoodType()
        {
            return db.Products.ToList().Where(p => p.CategoryID == 1).ToList();
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }


        public List<Product> ListFeatureProduct(int top)
        {
            List<Product> listproduct = (from data in db.Products
                                        where data.BoughtCount != null
                                        orderby data.BoughtCount descending
                                        select data).Take(top).ToList();
            return listproduct;
        }

        public List<Product> Listcombo(int top)
        {
            List<Product> listcombo = (from data in db.Products
                                       where data.CategoryID == 15 && data.Price < 500000
                                       select data).Take(top).ToList();
                                        
            return listcombo;
        }
        public List<Product> Listcombo1(int top)
        {
            List<Product> listcombo1 = (from data in db.Products
                                        where data.CategoryID == 13 &&  data.Price < 500000 
                                       select data).Take(top).ToList();

            return listcombo1;
        }
        public List<Product> Listcombo2(int top)
        {
            List<Product> listcombo2 = (from data in db.Products
                                       where data.CategoryID == 17 && data.Price < 500000
                                       select data).Take(top).ToList();

            return listcombo2;
        }
        public List<Product> ListRelatedProducts(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }


        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }

        public Product GetID(int id)
        {
            return db.Products.Find(id);
        }

        public Product GetTitle(string food)
        {
            return db.Products.Where(p => p.MetaTitle == food).First();
        }
        public Product GetName(string name)
        {
            return db.Products.Where(p => p.Name == name).SingleOrDefault();
        }

        public List<Product> GetIMG(string img)
        {
            return db.Products.Where(p => p.Image == img).ToList();
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Detail.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
