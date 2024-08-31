using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class AssetRepairResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<AssetRepairList> AssetRepairList { get; set; }
    }
    public class AssetRepairList
    {
        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string Asset_name { get; set; }
        public string Description { get; set; }
        public string ReasonFor_Repair { get; set; }
        public string ExpectedReturn_Date { get; set; }
        public string Vendor_Name { get; set; }
        public string Authorized_By { get; set; }
        public string Repair_Charge { get; set; }
        public int IsReturned { get; set; }
        public string Returned_On { get; set; }
        public string ReasonFor_Delay { get; set; }
        public string Location_Code { get; set; }
        public string Cdate { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
    }
}
