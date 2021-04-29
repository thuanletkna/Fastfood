using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunyStore.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Name { set; get; }
        
        public string GroupID { set; get; }
        public string Address { get; set; }

        
        public string Email { get; set; }

        
        public string Phone { get; set; }

        public string ProvinceID { get; set; }

        public string DistrictID { get; set; }
        public string PrecinctID { get; set; }

        public DateTime? CreatedDate { get; set; }

        
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

       
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }

        public string ResetPasswordCode { get; set; }
    }
}