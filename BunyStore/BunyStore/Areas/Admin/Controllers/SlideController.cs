using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetnoiCSDL.EF;
using PagedList;
using PagedList.Mvc;
using KetnoiCSDL.DAO;
using System.Data.Entity.Migrations;

namespace BunyStore.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        public static BunyStoreDbContext db = new BunyStoreDbContext();
        // GET: Admin/Slide
        public ActionResult Index()
        {
            
            
            
            return View(db.Slides.ToList());
        }

        //thêm slide
        [HttpGet]
        public ActionResult Themslide()
        {

            return View();
        }
        [HttpPost]
      
        public ActionResult Themslide(FormCollection collection, Slide slide)
        {
            var nametype = collection["Image"];
            var chitiet = collection["Description"];
            
            if (string.IsNullOrEmpty(nametype))
            {
                ViewData["Loi"] = "Vui lòng nhập tên loại sản phẩm";
            }
            else
            {
                slide.Image = nametype;
                slide.Description = chitiet;
                slide.CreatedDate = DateTime.Now;
                db.Slides.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Themslide();
        }

        // xóa slide
        //xóa sản phẩm
        [HttpGet]
        public ActionResult Xoaslide(int id)
        {
            Slide slide = db.Slides.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = slide.ID;
            if (slide == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(slide);
        }
        [HttpPost, ActionName("Xoaslide")]
        public ActionResult Xacnhanxoa(int id)
        {
            Slide slide = db.Slides.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = slide.ID;
            if (slide == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.Slides.Remove(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // sửa slide

        }

        //sửa slide
        [HttpGet]
        public ActionResult Suaslide(int id)
        {

            Slide slide = db.Slides.SingleOrDefault(n => n.ID == id);
            if (slide == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(slide);
        }

        [HttpPost]
        public ActionResult Suaslide(Slide slide)
        {

            if (slide == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                slide.CreatedDate = DateTime.Now;
                db.Slides.AddOrUpdate(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Suaslide(slide);
        }
    }
}