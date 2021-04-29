using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetnoiCSDL.EF;
using KetnoiCSDL.DAO;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity.Migrations;

namespace BunyStore.Areas.Admin.Controllers
{
    public class FoodTypeController : BaseController
    {
        public static BunyStoreDbContext db = new BunyStoreDbContext();
        

        //DANH SÁCH LOẠI SP
        public ActionResult ProductCategory(string searchString, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var dao = new ProductCategoryDao();
            var model = dao.ListAllPaging(searchString, pageNumber, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        //THÊM LOẠI SP
        [HttpGet]
        public ActionResult ThemloaiSp()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ThemloaiSp(FormCollection collection, ProductCategory productcategory)
        {
            var nametype = collection["Name"];
            if (string.IsNullOrEmpty(nametype))
            {
                ViewData["Loi"] = "Vui lòng nhập tên loại sản phẩm";
            }
            else{
                productcategory.Name = nametype;
                productcategory.CreatedDate = DateTime.Now;
                db.ProductCategories.Add(productcategory);
                db.SaveChanges();
                return RedirectToAction("ProductCategory");
            }
            return this.ThemloaiSp();
        }

        //CHI TIẾT LOẠI SP
        [HttpGet]
        public ActionResult ChitietloaiSp(int id)
        {
            ProductCategory productcategory = db.ProductCategories.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = productcategory.ID;
            if (productcategory == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(productcategory);
        }

        //XÓA LOẠI SP
        //xóa sản phẩm
        [HttpGet]
        public ActionResult XoaloaiSp(int id)
        {
            ProductCategory productcategory = db.ProductCategories.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = productcategory.ID;
            if (productcategory == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(productcategory);
        }
        [HttpPost, ActionName("XoaloaiSp")]
        public ActionResult Xacnhanxoa(int id)
        {
            ProductCategory productcategory = db.ProductCategories.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = productcategory.ID;
            if (productcategory == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.ProductCategories.Remove(productcategory);
                db.SaveChanges();
                return RedirectToAction("ProductCategory");
            }
        }

        //SỬA LOẠI SP
        [HttpGet]
        public ActionResult SualoaiSp(int id)
        {

            ProductCategory productcategory = db.ProductCategories.SingleOrDefault(n => n.ID == id);
            if (productcategory == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(productcategory);
        }

        [HttpPost]
        public ActionResult SualoaiSp(ProductCategory productcategory)
        {

            if (productcategory == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                productcategory.CreatedDate = DateTime.Now;
                db.ProductCategories.AddOrUpdate(productcategory);
                db.SaveChanges();
                return RedirectToAction("ProductCategory");
            }
            return this.SualoaiSp(productcategory);
        }
    }
}