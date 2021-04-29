using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BunyStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //danh mục sản phẩm
            routes.MapRoute(
               name: "Product Category",
               url: "san-pham/{metatitle}-{cateId}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //Gioi thieu
            routes.MapRoute(
               name: "Gioithieu",
               url: "gioi-thieu",
               defaults: new { controller = "GioiThieu", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            routes.MapRoute(
              name: "cua hang",
              url: "cua-hang",
              defaults: new { controller = "Cuahang", action = "Cuahang", id = UrlParameter.Optional },
              namespaces: new[] { "BunyStore.Controllers" }
          );
            routes.MapRoute(
               name: "khuyen-mai",
               url: "khuyen-mai",
               defaults: new { controller = "Khuyenmai", action = "Khuyenmai", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //Khuyến mãi
            routes.MapRoute(
               name: "chi-tiet-khuyen-mai",
               url: "chi-tiet-khuyen-mai/{meta}",
               defaults: new { controller = "Khuyenmai", action = "ChitietKM", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //chi tiết sản phẩm
            routes.MapRoute(
               name: "Product Detail",
               url: "chi-tiet/{metatitle}-{id}",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            routes.MapRoute(
              name: "User Detail",
              url: "xem-thong-tin/{metatitle}-{id}",
              defaults: new { controller = "User", action = "XemThongTin", id = UrlParameter.Optional },
              namespaces: new[] { "BunyStore.Controllers" }
          );

            //thêm giỏ hàng trả về danh sách loại 
            routes.MapRoute(
               name: "Add Cart Category",
               url: "them-gio-hang-loai",
               defaults: new { controller = "Cart", action = "AddItem_Catagory", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //thêm giỏ hàng trả về home
            routes.MapRoute(
               name: "Add Cart Home",
               url: "them-gio-hang-trang-chu",
               defaults: new { controller = "Cart", action = "AddItem_Home", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );
            routes.MapRoute(
              name: "Add Cart Chi tiet",
              url: "them-gio-hang-chi-tiet",
              defaults: new { controller = "Cart", action = "AddItem_chitiet", id = UrlParameter.Optional },
              namespaces: new[] { "BunyStore.Controllers" }
          );

            //giỏ hàng
            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            routes.MapRoute(
               name: "Edit",
               url: "edit-profile",
               defaults: new { controller = "EditUser", action = "Edit", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //thanh toán không tài khoản
            routes.MapRoute(
               name: "_PaymentNoAccount",
               url: "thanh-toan-khong-tai-khoan",
               defaults: new { controller = "Cart", action = "PaymentNoAccount", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );
            //thanh toán bằng tài khoản
            routes.MapRoute(
               name: "_PaymentWithAccount",
               url: "thanh-toan-bang-tai-khoan",
               defaults: new { controller = "Cart", action = "PaymentWithAccount", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //thanh toán thành công
            routes.MapRoute(
               name: "payment Success",
               url: "hoan-thanh",
               defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //giới thiệu
            routes.MapRoute(
               name: "About",
               url: "gioi-thieu",
               defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //liên hệ
            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            //tìm kiếm
            routes.MapRoute(
               name: "timkiem",
               url: "tim-kiem",
               defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            routes.MapRoute(
               name: "Register",
               url: "dang-ky",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );
            routes.MapRoute(
               name: "Login",
               url: "dang-nhap",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );
            //xemsanpham
            routes.MapRoute(
              name: "User",
              url: "xem-thong-tin",
              defaults: new { controller = "User", action = "XemThongTin", id = UrlParameter.Optional },
              namespaces: new[] { "BunyStore.Controllers" }
          );
            routes.MapRoute(
              name: "suainfo",
              url: "sua-thong-tin",
              defaults: new { controller = "User", action = "SuaThongTin", id = UrlParameter.Optional },
              namespaces: new[] { "BunyStore.Controllers" }
          );



            routes.MapRoute(
               name: "ForgotPassword",
               url: "quen-mat-khau",
               defaults: new { controller = "User", action = "ForgotPassword", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            routes.MapRoute(
               name: "ResetPassword",
               url: "thay-doi-mat-khau/{id}",
               defaults: new { controller = "User", action = "ResetPassword", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            routes.MapRoute(
               name: "ChangePassword",
               url: "change-mat-khau/{id}",
               defaults: new { controller = "User", action = "ChangePassword", id = UrlParameter.Optional },
               namespaces: new[] { "BunyStore.Controllers" }
           );

            routes.MapRoute(
                name: "trangchu",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BunyStore.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BunyStore.Controllers" }
            );
        }
    }
}
