using BunyStore.Areas.Admin.Models;
using BunyStore.Common;
using KetnoiCSDL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BunyStore.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        // GET: Admin/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, FormCollection collection)
        {
            
            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.PassWord))
            {
                var dao = new Admindao();
                var result = new Admindao().Login(model.UserName, model.PassWord);

                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.ADMIN_SESSION, userSession);
                    return RedirectToAction("Product", "Home");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                //Tài khoản tồn tại nhưng sai mật khẩu
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.ADMIN_SESSION] = null;
            return Redirect("/Admin/Login/Login");
        }
    }
}