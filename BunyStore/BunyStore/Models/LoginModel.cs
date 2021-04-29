using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BunyStore.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn phải nhập tài khoản")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Tên đăng nhập từ 6-20 kí tự.")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6-20 kí tự.")]
        [Display(Name = "Mật khẩu")]
        public string Password { set; get; }
    }
}