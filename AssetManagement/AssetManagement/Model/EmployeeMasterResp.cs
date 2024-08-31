using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class EmployeeMasterResp
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<string> Employee { get; set; }
    }
}
