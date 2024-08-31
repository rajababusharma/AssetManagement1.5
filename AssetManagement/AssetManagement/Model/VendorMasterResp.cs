using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class VendorMasterResp
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<string> Vendor { get; set; }
    }
}
