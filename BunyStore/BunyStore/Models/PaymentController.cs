using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BunyStore.Models
{
    public class PaymentController
    {
        [Key]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string Name { set; get; }
        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }
        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [Display(Name = "Email")]
        public string Email { set; get; }
        [Display(Name = "Điện thoại")]
        public string Phone { set; get; }
    }
}