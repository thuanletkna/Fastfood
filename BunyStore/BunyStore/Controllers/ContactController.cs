using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetnoiCSDL.EF;
using KetnoiCSDL.DAO;
using BunyStore.Models;
using KetnoiCSDL.Service;
using AutoMapper;
using BunyStore.Common;
using BunyStore.Infrastructure.Extensions;
using BunyStore.Helper;

namespace BunyStore.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        BunyStoreDbContext db = new BunyStoreDbContext();
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            ViewBag.SuccessMsg = CommonConstants.feedbackStatus;
            CommonConstants.feedbackStatus = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FeedbackModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(feedbackViewModel);
                newFeedback.Status = false; // các phản hồi chưa được xem tình trạng false
                newFeedback.CreatedDate = DateTime.Now;
                //db.Feedbacks.Add(newFeedback);
                //db.SaveChanges();
                CommonConstants.feedbackStatus = "Gửi phản hồi thành công";
                string content = System.IO.File.ReadAllText(Server.MapPath("/assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Phone}}", feedbackViewModel.Phone);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Address}}", feedbackViewModel.Address);
                content = content.Replace("{{Content}}", feedbackViewModel.Content);
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);
            }
            //feedbackViewModel.ContactDetail = GetDetail();
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }
    }
}