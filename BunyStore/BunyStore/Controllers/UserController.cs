using BunyStore.Common;
using BunyStore.Models;
using KetnoiCSDL.DAO;
using KetnoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Data.Entity.Migrations;
using System.Xml.Linq;
using BunnyStore.Models;

namespace BunyStore.Controllers
{
    public class UserController : Controller
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
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Password = Cryptography.CreateMD5(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;                   
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    if (!string.IsNullOrEmpty(model.ProvinceID))
                    {
                        user.ProvinceID = model.ProvinceID;
                    }
                    if (!string.IsNullOrEmpty(model.DistrictID))
                    {
                        user.DistrictID = model.DistrictID;
                    }
                    if (!string.IsNullOrEmpty(model.PrecinctID))
                    {
                        user.PrecinctID = model.PrecinctID;
                    }

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        SetAlert("Đăng ký thành công", "success");
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                        return Redirect("dang-nhap");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View("Register");
        }

        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult LoadDistrict(string provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && (x.Attribute("value").Value) == provinceID);

            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(district);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }

        public JsonResult LoadPrecinctID(string precientID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item").Elements("Item")
                .Single(x => x.Attribute("type").Value == "district" && (x.Attribute("value").Value) == precientID);

            var list = new List<PrecientModel>();
            PrecientModel precient = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "precinct"))
            {
                precient = new PrecientModel();
                precient.ID = int.Parse(item.Attribute("id").Value);
                precient.Name = item.Attribute("value").Value;
                precient.DistrictID = int.Parse(xElement.Attribute("id").Value);
                list.Add(precient);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName,  Cryptography.CreateMD5(model.Password).ToLower());
                if (result  && ModelState.IsValid)
                {
                    var user = dao.GetByUserName(model.UserName);
                    
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.Email = user.Email;
                    userSession.Phone = user.Phone;
                    userSession.Name = user.Name;
                    userSession.Status = user.Status;
                    userSession.Address = user.Address;
                    userSession.DistrictID = user.DistrictID;
                    userSession.ProvinceID = user.ProvinceID;
                    userSession.PrecinctID = user.PrecinctID;
                    userSession.CreatedDate = user.CreatedDate;
                    userSession.Password = user.Password;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    CommonConstants.CheckLogin = user;
                    CommonConstants.loginUser = user;
                    
                    return Redirect("/");
                }
                
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View();
        }


        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                string phone = me.phone;

                var user = new User();
                user.Email = email;
                user.UserName = email;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.CreatedDate = DateTime.Now;
                user.Phone = phone;
                var resultInsert = new UserDao().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.Name = user.Name;
                    userSession.Phone = user.Phone;
                    userSession.CreatedDate = user.CreatedDate;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            CommonConstants.CheckLogin = null;
            return Redirect("/");
        }


        //phan quen mat khau
        public bool IsEmailExist(string emailID)
        {
            using (BunyStoreDbContext dc = new BunyStoreDbContext())
            {
                var v = dc.Users.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/User/ResetPassword/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("lthuan217@gmail.com", "Thay đổi mật khẩu");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Thuan1711061458"; // Replace with actual password
            string subject = "Reset Password";

            string body = "Thay đổi mật khẩu <br/>br/> Vui lòng bấm vào đường link để thay đổi mật khẩu" +
            "<br/><br/><a href=" + link + ">Reset Password link</a>";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                

            Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)

            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string emailID)
        {
            string message = "";
            bool status = false;
            using (BunyStoreDbContext dc = new BunyStoreDbContext())
            {
                var account = dc.Users.Where(a => a.Email == emailID).FirstOrDefault();
                if(account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.Email, resetCode);
                    account.ResetPasswordCode = resetCode;
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Đường link đổi mật khẩu đã được gửi vào mail của bạn";
                }
                else
                {
                    message = "Tài khoản không tồn tại";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (BunyStoreDbContext dc = new BunyStoreDbContext())
            {
                var user = dc.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (BunyStoreDbContext dc = new BunyStoreDbContext())
                {
                    var user = dc.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Cryptography.CreateMD5(model.NewPassword);
                        user.ResetPasswordCode = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "Mật khẩu đã thay đổi thành công";
                    }
                }
            }
            else
            {
                message = "Mật khẩu và xác nhận mật khẩu phải giống nhau";
            }
            ViewBag.Message = message;
            return View(model);
        }
        [HttpGet]
        public ActionResult SuaThongTin()
        {
            
            return View();
            
        }
        [HttpPost]
        public ActionResult SuaThongTin(string emailID)
        {
            string message = "";
            bool status = false;
            using (BunyStoreDbContext dc = new BunyStoreDbContext())
            {
                var account = dc.Users.Where(a => a.Email == emailID).FirstOrDefault();
                if (account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    LinkReset(account.Email, resetCode);
                    account.ResetPasswordCode = resetCode;
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Đường link đổi mật khẩu đã được gửi vào mail của bạn";
                }
                else
                {
                    message = "Tài khoản không tồn tại";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ChangePassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (BunyStoreDbContext dc = new BunyStoreDbContext())
            {
                var user = dc.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (BunyStoreDbContext dc = new BunyStoreDbContext())
                {
                    var user = dc.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Cryptography.CreateMD5(model.NewPassword);
                        user.ResetPasswordCode = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "Mật khẩu đã thay đổi thành công";
                    }
                }
            }
            else
            {
                message = "Mật khẩu và xác nhận mật khẩu phải giống nhau";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [NonAction]
        public void LinkReset(string emailID, string activationCode)
        {
            var verify = "/User/ChangePassword/" + activationCode;
            var linkchange = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verify);

            var fromEmail = new MailAddress("247thucannhanh@gmail.com", "Thay đổi mật khẩu");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "2506271099a"; // Replace with actual password
            string subject = "Reset Password";

            string body = "Thay đổi mật khẩu <br/>br/> Vui lòng bấm vào đường link để thay đổi mật khẩu" +
            "<br/><br/><a href=" + linkchange + ">Reset Password link</a>";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        public ActionResult XemThongTin()
        {
            var user = Session["loginUser"];
            ViewBag.loginUser = user;
            return View();
        }

        public ActionResult XuLyMatKhau(int id, string pass, string adress)
        {
            var user = db.Users.Where(p => p.ID == id).SingleOrDefault();
            var userSession = new UserLogin();
            userSession.UserName = user.UserName;
            userSession.Password = pass;
            userSession.Name = user.Name;
            userSession.Phone = user.Phone;
            userSession.Email = user.Email;
            userSession.Address = adress;
            userSession.CreatedDate = DateTime.Now;
            userSession.UserID = user.ID;

            user.Address = adress;
            user.Password = pass;
            db.Users.AddOrUpdate(user);
            db.SaveChanges();

            Session.Add(CommonConstants.USER_SESSION, userSession);
            CommonConstants.loginUser = user;

            return RedirectToAction("XemThongTin");
        }

        public ActionResult XemThongTinDonHang(string searchString, int? page)
        {
            var user = (BunyStore.Common.UserLogin)Session[BunyStore.Common.CommonConstants.USER_SESSION];

            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var dao = new OrderDao();

            var model = dao.ListAllPagingAll(searchString, pageNumber, pageSize, int.Parse(user.UserID.ToString()));
            return View(model);
        }
    }

}