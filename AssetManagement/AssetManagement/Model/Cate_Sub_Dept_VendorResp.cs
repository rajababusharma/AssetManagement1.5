using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class Cate_Sub_Dept_VendorResp
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<string> CategoryList { get; set; }
        public List<string> SubCategoryList { get; set; }
        public List<string> DepartmentList { get; set; }
        public List<string> VendorList { get; set; }
        public List<string> BranchList { get; set; }
        public List<string> LocationList { get; set; }
    }
}
