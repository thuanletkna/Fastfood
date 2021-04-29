using BunyStore.Common;
using KetnoiCSDL.DAO;
using KetnoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BunyStore.Controllers
{
    public class CuahangController : Controller
    {
        private BunyStoreDbContext data = new BunyStoreDbContext();
        // GET: Cuahang
        public ActionResult Cuahang()
        {
            CommonConstants.Tinh = null;
            var list = data.Stores.ToList();
            ViewData["Tinh"] = new SelectList(data.Provinces.OrderBy(p => p.nameProvince).ToList(), "id", "nameProvince");
            return View(list);
        }

        [HttpPost]
        public ActionResult Cuahang(FormCollection collection, string meta)
        {
            var tinh = int.Parse(collection["Tinh"]);
            //Khai báo 1 biến lấy ra Tỉnh thành mà người dùng chọn
            var temp = new ProvinceDAO().GetProvince(tinh);
            CommonConstants.Tinh = temp.nameProvince;
            var list = data.Stores.Where(p => p.Province.id == tinh).ToList();
            ViewData["Tinh"] = new SelectList(data.Provinces.OrderBy(p => p.nameProvince).ToList(), "id", "nameProvince");
            return PartialView(list);
        }
    }
}