using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace BunyStore.Models
{
    public class ChangeinfoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { set; get; }

        //Tên đang nhập
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string UserName { set; get; }


        //Mật khẩu
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6-20 kí tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { set; get; }


        //Xác nhận mật khẩu
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string Name { set; get; }

        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email phải có đuôi @gmail.com")]
        [Display(Name = "Email")]
        public string Email { set; get; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Display(Name = "Điện thoại")]
        public string Phone { set; get; }

        [Display(Name = "Tỉnh/thành")]
        public string ProvinceID { set; get; }

        [Display(Name = "Quận/Quyện")]
        public string DistrictID { set; get; }
    }
}
