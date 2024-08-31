using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class PostAssetRepairRequest
    {
        public string Company_Id { get; set; }
        public string FinancialYear { get; set; }
        public string Asset_id { get; set; }
        public string Asset_name { get; set; }
        public string Description { get; set; }
        public string ReasonFor_Repair { get; set; }
        public string ExpectedReturn_Date { get; set; }
        public string Vendor_Name { get; set; }
        public string Authorized_By { get; set; }
        public string Cdate { get; set; }
        public string Location_Code { get; set; }
        public string Remarks { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
    }
}
