using KetnoiCSDL.DAO;
using BunyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using KetnoiCSDL.EF;
using BunyStore.Common;
using System.Configuration;
using System.IO;
namespace BunyStore.Controllers
{
    public class CartController : Controller
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
        public static BunyStoreDbContext db = new BunyStoreDbContext();
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            //12312321312
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        //update giỏ hàng
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.Where(x => x.Product.ID == item.Product.ID).SingleOrDefault();
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        //xóa all giỏ hàng
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        // xóa từng món hàng
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        //thêm item vào giỏ hàng và trả về trang loại hiện tại
        public ActionResult AddItem_Catagory(long productId, int quantity, int page)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Category", "Product", new { cateID = product.CategoryID, page = page });
        }


        //thêm item vào giỏ hàng và trả về trang index
        public ActionResult AddItem_Home(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index", "Home");
        }

        //thêm item vào giỏ hàng và trả về trang index
        public ActionResult AddItem_chitiet(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Detail", "Product");
        }

        [HttpGet]
        public ActionResult PaymentNoAccount()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            // nếu không login chuyển về trang login
            if (session == null)
            {
                SetAlert("Bạn cần phải đăng nhập để thanh toán đơn hàng", "error");
                return RedirectToAction("Login", "User");
            }
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }


        [HttpPost]
        public ActionResult PaymentNoAccount(string shipName, string mobile, string address, string email)
        {
            Common.CommonConstants.Count = 0;
            //var user = new User();
            //user.Name = shipName;
            //user.Email = email;
            //user.Phone = mobile;
            //user.Address = address;

            CustomerModel temp = new CustomerModel();
            temp.shipName = shipName;
            temp.Phone = mobile;
            temp.Email = email;
            temp.Address = address;

            CommonConstants.CustomerTemp = temp;
            return RedirectToAction("PaymentWithAccount", temp);
        }


        //Trang thanh toán bằng tài khoản và trang hiển thị thông tin đơn hàng gộp lại thành 1
        public ActionResult PaymentWithAccount(CustomerModel user)
        {
            //CommonConstants.linkPayment = Request.Url.Segments[3].ToString()+"/"+Request.Url.Query.ToString();

            //Lấy ra session chứa thông tin người dùng
            var user_temp = (BunyStore.Common.UserLogin)Session[BunyStore.Common.CommonConstants.USER_SESSION];
            CustomerModel temp = new CustomerModel();

            //Trường hợp server trả null thì gán giá trị bằng với session khởi tạo
            if (user.shipName == null || user.Phone == null || user.Email == null || user.Address == null || user.DistrictID == null || user.ProvinceID == null || user.PrecinctID == null)
            {
                temp.shipName = user_temp.Name;
                temp.Phone = user_temp.Phone;
                temp.Email = user_temp.Email;
                temp.Address = user_temp.Address;
                temp.DistrictID = user_temp.DistrictID;
                temp.ProvinceID = user_temp.ProvinceID;
                temp.PrecinctID = user_temp.PrecinctID;
            }
            //Ngược lại gán giá trị bằng user lấy từ server
            else
            {
                temp.shipName = user.shipName;
                temp.Phone = user.Phone;
                temp.Email = user.Email;
                temp.Address = user.Address;
                temp.DistrictID = user.DistrictID;
                temp.ProvinceID = user.ProvinceID;
                temp.PrecinctID = user.PrecinctID;
            }


            CommonConstants.CustomerTemp = temp;

            return PartialView("PaymentWithAccount", temp);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}