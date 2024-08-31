using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class UserMasterResp
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<string> Users { get; set; }
    }
}
