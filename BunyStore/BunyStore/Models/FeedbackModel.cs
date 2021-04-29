using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BunyStore.Models
{
    public class FeedbackModel
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Yêu cầu nhập tên liên hệ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        [StringLength(10 ,ErrorMessage = "Số điện thoại gồm 10 số")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email phải có đuôi @gmail.com")]
        [StringLength(50, ErrorMessage = "Email chỉ được phép 50 kí tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        [StringLength(50, ErrorMessage = "Địa chỉ tối đa 50 kí tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập nội dung liên hệ")]
        [StringLength(250, ErrorMessage = "Nội dung liên hệ tối đa 250 kí tự")]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }

        public ContactModel ContactDetail { get; set; }
    }
}