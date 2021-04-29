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
    public class NguoidungController : BaseController
    {
        public static BunyStoreDbContext db = new BunyStoreDbContext();
        // GET: Admin/Nguoidung

        // Danh sách người dùng
        public ActionResult Nguoidung(string searchString, int? page)

        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, pageNumber, pageSize);

            ViewBag.SearchString = searchString;
            
            return View(model);
        }

        //Thêm Người dùng
        [HttpGet]
        public ActionResult ThemND()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ThemND(FormCollection collection, User user)
        {
            var tennd = collection["Name"];
            var tendn = collection["Username"]; 
            var pass = collection["Password"];
            var dc = collection["Adress"];
            var email = collection["Email"];
            var sdt = collection["Phone"];
            
            if (string.IsNullOrEmpty(tendn.ToString()))
            {
                ViewData["Loi1"] = "vui lòng nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(pass))
            {

                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }
            else if (string.IsNullOrEmpty(tennd))
            {
                ViewData["Loi"] = "Vui lòng nhập tên người dùng";
            }
            else if (string.IsNullOrEmpty(email))
            {

                ViewData["Loi5"] = "Vui lòng nhập email";
            }
            else if (string.IsNullOrEmpty(sdt))
            {

                ViewData["Loi6"] = "Vui lòng nhập số điện thoại";
            }
            else
            {
                user.UserName = tendn;
                user.Password = pass;
                user.Address = dc;
                user.Name = tennd;
                user.Email = email;
                user.Phone = sdt;
                user.CreatedDate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Nguoidung");
            }
            return this.ThemND();
        }

        //chi tiết người dùng

       
        [HttpGet]
        public ActionResult ChitietND(int id)
        {
            User user = db.Users.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = user.ID;
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }

        //xóa người dùng
        [HttpGet]
        public ActionResult XoaND(int id)
        {
            User user = db.Users.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = user.ID;
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }
        [HttpPost, ActionName("XoaND")]
        public ActionResult Xacnhanxoa(int id)
        {
            User user = db.Users.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = user.ID;
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Nguoidung");
            }
            return this.ThemND();
        }

        //SỬA người dùng
        [HttpGet]
        public ActionResult SuaND(int id)
        {

            User user = db.Users.SingleOrDefault(n => n.ID == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult SuaND(User user)
        {

            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
               
                user.CreatedDate = DateTime.Now;
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
                return RedirectToAction("Nguoidung");
            }
        }
    }
}