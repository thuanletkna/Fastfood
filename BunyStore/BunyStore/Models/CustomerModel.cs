using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunyStore.Models
{
    public class CustomerModel
    {
        public string shipName { set; get; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        public string PrecinctID { get; set; }
        public string DistrictID { get; set; }
        public string ProvinceID { get; set; }


    }
}