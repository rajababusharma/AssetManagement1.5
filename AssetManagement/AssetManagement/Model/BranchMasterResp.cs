using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class BranchMasterResp
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<string> Branches { get; set; }
    }
}
