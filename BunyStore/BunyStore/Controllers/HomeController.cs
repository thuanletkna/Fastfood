using BunyStore.Common;
using BunyStore.Models;
using KetnoiCSDL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BunyStore.Controllers
{
    public class HomeController : Controller
    {
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();
            ViewBag.Khuyenmai = new DiscountDAO().ListAll();
            var model = new ProductCategoryDao().ListAll();
            ViewBag.ProductCategory = new ProductCategoryDao().ListAll();
            var productcategoryDao = new ProductCategoryDao();
            var productDao = new ProductDao();
            
            ViewBag.NewProducts = productDao.ListNewProduct(8);
            ViewBag.ListFeatureProducts = productDao.ListFeatureProduct(8);
            ViewBag.NewProductcategory = productcategoryDao.ListNewProductcategory(3);
            ViewBag.listproduct = productDao.ListByCategoryId(8);
            ViewBag.listcombo = productDao.Listcombo(9);
            ViewBag.listcombo1 = productDao.Listcombo1(9);
            ViewBag.combonhom = productcategoryDao.Combonhom(1);
            ViewBag.combonhom1 = productcategoryDao.Combonhom1(1);
            ViewBag.combonhom2 = productcategoryDao.Combonhom2(1);
            ViewBag.listcombo2 = productDao.Listcombo2(9);
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupID(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult MainMenuRespon()
        {
            var model = new MenuDao().ListByGroupID(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupID(2);
            return PartialView(model);
        }
        

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDao().LayFooter();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart1()
        {
            
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {

                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
        [ChildActionOnly]
        public ActionResult Toplogin()
        {
            var model = new MenuDao().ListByGroupID(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult Toploginrespon()
        {
            var model = new MenuDao().ListByGroupID(2);
            return PartialView(model);
        }

    }
}