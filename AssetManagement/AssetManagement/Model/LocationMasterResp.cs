using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class LocationMasterResp
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<string> Locations { get; set; }
    }
}
