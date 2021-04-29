using KetnoiCSDL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BunyStore.Controllers
{
    public class KhuyenmaiController : Controller
    {
        // GET: Khuyenmai
        public ActionResult Khuyenmai()
        {
            var discount = new DiscountDAO().ListAll();
            return View(discount);
        }

        public ActionResult ChitietKM(string meta)
        {
            var km = new DiscountDAO().GetDisCount(meta);
            return View(km);
        }
    }
}