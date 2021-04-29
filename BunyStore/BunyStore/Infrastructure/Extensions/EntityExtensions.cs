using BunyStore.Models;
using KetnoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunyStore.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateFeedback(this Feedback feedback, FeedbackModel feedbackVm)
        {
            feedback.Name = feedbackVm.Name;
            feedback.Email = feedbackVm.Email;
            feedback.Phone = feedbackVm.Phone;
            feedback.Content = feedbackVm.Content;
            feedback.Address = feedbackVm.Address;
        }
    }
}