using KetnoiCSDL.DAO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BunyStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Category(long cateId, int? page)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            var model = new ProductDao().ListByCategoryId(cateId);
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            ViewBag.page = pageNumber;
            ViewBag.cateID = cateId;
            return View((model.ToList().OrderBy(n => n.ID)).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Detail(long id)
        {
           
            var product = new ProductDao().ViewDetail(id);
            
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(8);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(id);
            return View(product);
        }
       

        

    }
}