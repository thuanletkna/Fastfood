using BunyStore.Models;
using KetnoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunyStore.Common
{
    public static class CommonConstants
    {
        public static string USER_SESSION = "USER_SESSION";
        public static string ADMIN_SESSION = "ADMIN_SESSION";
        public static string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";
        public static string CartSession = "CartSession";
        public static string CurrentCulture { set; get; }
        //Kiểm tra tình trạng giỏ hàng
        public static string StatusCard { set; get; }
        //Lưu số thức ăn trong giỏ hàng 
        public static int Count { set; get; }
        public static string Tinh { set; get; }

        //Tình trạng phản hồi
        public static string feedbackStatus { set; get; }

        //Kiểm tra trạng thái đăng nhập
        public static User CheckLogin { set; get; }
        public static User loginUser
        {
            get
            {
                return (User)HttpContext.Current.Session["loginUser"];
            }
            set
            {
                HttpContext.Current.Session["loginUser"] = value;
            }
        }

        public static User loginAmin
        {
            get
            {
                return (User)HttpContext.Current.Session["loginAmin"];
            }
            set
            {
                HttpContext.Current.Session["loginAmin"] = value;
            }
        }

        //Lấy đường link để truyền vào thanh toán
        public static string linkPayment { set; get; }

        public static CustomerModel CustomerTemp { set; get; }
    }
}