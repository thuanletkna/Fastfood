using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KetnoiCSDL.DAO;
using KetnoiCSDL.EF;

namespace BunyStore.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private BunyStoreDbContext db = new BunyStoreDbContext();

        //DANH SÁCH TẤT CẢ ĐƠN HÀNG
        public ActionResult OrderListAll(string searchString, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var dao = new OrderDao();
            var model = dao.ListAllPagingAll(searchString, pageNumber, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        //DANH SÁCH ĐƠN HÀNG CHƯA XỬ LÝ
        public ActionResult OrderListAllFalse(string searchString, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var dao = new OrderDao();
            var model = dao.ListAllPagingFalse(searchString, pageNumber, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }


        //CHI TIẾT ĐƠN HÀNG
        [HttpGet]
        public ActionResult DetailsOrderFalse(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        //XÓA ĐƠN HÀNG
        [HttpGet]
        public ActionResult DeleteOrderFalse(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        [HttpPost]
        public JsonResult AccessDeleteFalse(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                return Json(new { status = true });
            }
        }

        //SỬA LOẠI SP
        [HttpGet]
        public ActionResult EditOrderFalse(int id)
        {

            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult EditOrderFalse(int id, string status)
        {
            var ordertemp = db.Orders.Where(p => p.ID == id).SingleOrDefault();
            if (ordertemp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                if (status != null)
                {
                    ordertemp.Status = status;
                    ordertemp.CreatedDate = DateTime.Now;
                    db.Orders.AddOrUpdate(ordertemp);
                    db.SaveChanges();
                }

                return RedirectToAction("OrderListAllFalse", 1);
            }
        }



        //DANH SÁCH ĐƠN HÀNG ĐÃ XỬ LÝ
        public ActionResult OrderListAllTrue(string searchString, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var dao = new OrderDao();
            var model = dao.ListAllPagingTrue(searchString, pageNumber, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }


        //CHI TIẾT ĐƠN HÀNG
        [HttpGet]
        public ActionResult DetailsOrderTrue(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        //XÓA ĐƠN HÀNG
        [HttpGet]
        public ActionResult DeleteOrderTrue(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        [HttpPost]
        public JsonResult AccessDeleteTrue(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                return Json(new { status = true });
            }
        }

        //Sửa đơn hàng
        [HttpGet]
        public ActionResult EditOrderTrue(int id)
        {

            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult EditOrderTrue(int id, string status)
        {
            var ordertemp = db.Orders.Where(p => p.ID == id).SingleOrDefault();
            if (ordertemp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                if (status != null)
                {
                    ordertemp.Status = status;
                    ordertemp.CreatedDate = DateTime.Now;
                    db.Orders.AddOrUpdate(ordertemp);
                    db.SaveChanges();
                }

                return RedirectToAction("OrderListAllTrue", 1);
            }
        }

        //---------------------------------------------------------------------------------------------

        //DANH SÁCH ĐƠN HÀNG ĐANG CHỜ XỬ LÝ
        public ActionResult OrderListAllCheck(string searchString, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var dao = new OrderDao();
            var model = dao.ListAllPagingCheck(searchString, pageNumber, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }


        //CHI TIẾT ĐƠN HÀNG
        [HttpGet]
        public ActionResult DetailsOrderCheck(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        //XÓA ĐƠN HÀNG
        [HttpGet]
        public ActionResult DeleteOrderCheck(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        [HttpPost]
        public JsonResult AccessDeleteCheck(int id)
        {
            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = order.ID;
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                return Json(new { status = true });
            }
        }

        //Sửa đơn hàng
        [HttpGet]
        public ActionResult EditOrderCheck(int id)
        {

            Order order = db.Orders.SingleOrDefault(n => n.ID == id);
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult EditOrderCheck(int id, string status)
        {
            var ordertemp = db.Orders.Where(p => p.ID == id).SingleOrDefault();
            if (ordertemp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                if (status != null)
                {
                    ordertemp.Status = status;
                    ordertemp.CreatedDate = DateTime.Now;
                    db.Orders.AddOrUpdate(ordertemp);
                    db.SaveChanges();
                }

                return RedirectToAction("OrderListAllCheck", 1);
            }
        }
    }
}
