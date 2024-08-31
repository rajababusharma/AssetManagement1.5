using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class CreateVendorRequest
    {
        public string Company_Id { get; set; }
        public string Vendor_code { get; set; }
        public string Vendor_Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Contact { get; set; }
        public string EmailId { get; set; }
        public string gstno { get; set; }
    }
}
