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
using System.IO;

namespace BunyStore.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public static BunyStoreDbContext db = new BunyStoreDbContext();
        // GET: Admin/Home

        public ActionResult Chart()
        {
            //khai báo doanh thu các tháng
            decimal th1 = 0, th2 = 0, th3 = 0, th4 = 0, th5 = 0, th6 = 0, th7 = 0, th8 = 0, th9 = 0, th10 = 0, th11 = 0, th12 = 0;

            //Khai báo các tháng
            var List = db.Orders.ToList();
            //Khai bao so luong tung loai thuc an


            //Lọc ra danh sách trong 12 tháng
            foreach (var item in List)
            {
                for (int i = 1; i <= 12; i++)
                {
                    //chọn ra tháng trùng với điều kiện và năm hiện tại
                    if (item.CreatedDate.Value.Month == i && item.CreatedDate.Value.Year == 2021)
                    {
                        // Lấy ra danh các đặt hàng dò với chi tiết để có giá tiền
                        var count = db.OrderDetails.Where(p => p.OrderID == item.ID).ToList();

                        //Nếu có doanh thu trong khoảng thời gian này
                        //Tiến hành cộng số tiền các đơn hàng
                        if (count.Count > 0)
                        {
                            foreach (var item1 in count)
                            {
                                decimal thang = 0;
                                thang += decimal.Parse(item1.Price.ToString()) * decimal.Parse(item1.Quantity.ToString());
                                //Kiểm tra tháng của đơn hàng hiện tại để cộng đơn hàng vào
                                if (i == 1)
                                {
                                    th1 += thang;
                                    ViewData["TH" + i.ToString()] = th1;
                                }
                                if (i == 2)
                                {
                                    th2 += thang;
                                    ViewData["TH" + i.ToString()] = th2;
                                }
                                if (i == 3)
                                {
                                    th3 += thang;
                                    ViewData["TH" + i.ToString()] = th3;
                                }
                                if (i == 4)
                                {
                                    th4 += thang;
                                    ViewData["TH" + i.ToString()] = th4;
                                }
                                if (i == 5)
                                {
                                    th5 += thang;
                                    ViewData["TH" + i.ToString()] = th5;
                                }
                                if (i == 6)
                                {
                                    th6 += thang;
                                    ViewData["TH" + i.ToString()] = th6;
                                }
                                if (i == 7)
                                {
                                    th7 += thang;
                                    ViewData["TH" + i.ToString()] = th7;
                                }
                                if (i == 8)
                                {
                                    th8 += thang;
                                    ViewData["TH" + i.ToString()] = th8;
                                }
                                if (i == 9)
                                {
                                    th9 += thang;
                                    ViewData["TH" + i.ToString()] = th9;
                                }
                                if (i == 10)
                                {
                                    th10 += thang;
                                    ViewData["TH" + i.ToString()] = th10;
                                }
                                if (i == 11)
                                {
                                    th11 += thang;
                                    ViewData["TH" + i.ToString()] = th11;
                                }
                                if (i == 12)
                                {
                                    th12 += thang;
                                    ViewData["TH" + i.ToString()] = th12;
                                }
                            }
                        }
                        //Nếu không thì gán cho giá trị được khởi tạo ban đầu = 0
                        else
                        {
                            ViewData["TH" + i.ToString()] = 0;
                        }
                    }

                }
            }

            return View();
        }

        public ActionResult Product(string searchString,int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, pageNumber, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name");
        }

        [HttpGet]
        public ActionResult ThemmoiSp()
        {
            
            SetViewBag();
            return View();
        }
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult ThemmoiSp(Product product, FormCollection collection)
        {
            
            var foodname = collection["Name"];
            var meta = collection["MetaTitle"];
            var price = collection["Price"];
            var details = collection["Detail"];
            var soluong = collection["Quantity"];
            var anh = collection["Image"];
            if (string.IsNullOrEmpty(foodname))
            {
                ViewData["Loi"] = "Vui lòng nhập tên món";
            }
            else if (string.IsNullOrEmpty(price.ToString()))
            {
                ViewData["Loi1"] = "vui lòng nhập giá bán";
            }
            else if (string.IsNullOrEmpty(details))
            {

                ViewData["Loi3"] = "Vui lòng nhập chi tiết cho món";
            }
            else if (string.IsNullOrEmpty(soluong))
            {

                ViewData["Loi2"] = "Vui lòng nhập số lượng cho món";
            }
            else if (string.IsNullOrEmpty(anh))
            {

                ViewData["Loi5"] = "Vui lòng chọn ảnh cho món";
            }
            else
            {
                
                product.Name = foodname;
                product.Price = int.Parse(price);
                product.MetaTitle = meta;
                product.Detail = details;
                product.Quantity = int.Parse(soluong);
                product.Image = anh;
                product.CreatedDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                SetViewBag();
                return RedirectToAction("Product");
            }
            return this.ThemmoiSp();
        }

        //chi tiết sp
        [HttpGet]
        public ActionResult ChitietSp(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = product.ID;
            if(product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        
        //xóa sản phẩm
        [HttpGet]
        public ActionResult XoaSp(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = product.ID;
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        [HttpPost, ActionName("XoaSp")]
        public ActionResult Xacnhanxoa(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = product.ID;
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Product");
            }
            return this.ThemmoiSp();
        }

        //sửa sp
        [HttpGet]
        public ActionResult SuaSp(int id)
        {
            SetViewBag();
            Product product = db.Products.SingleOrDefault(n => n.ID == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult SuaSp(Product product)
        {
            
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                
                SetViewBag();
                product.CreatedDate = DateTime.Now;
                db.Products.AddOrUpdate(product);
                db.SaveChanges();
                return RedirectToAction("Product");
            }
        }


        
    }
}