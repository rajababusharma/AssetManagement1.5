using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class CreateAssetRequest
    {
        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string FinancialYear { get; set; }
        public string Asset_name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Branch { get; set; }
        public string Employee { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Asset_type { get; set; }
        public string Asset_value { get; set; }
        public int Asset_life { get; set; }
        public string Vendor { get; set; }
        public string SAP_code { get; set; }
        public string Purchase_date { get; set; }
        public string Install_date { get; set; }
        public string ManufacturedBy { get; set; }
        public string Mfd_date { get; set; }
        public int Warranty_period { get; set; }
        public string Model_no { get; set; }
        public string Part_no { get; set; }
        public string Serial_no { get; set; }
        public string Current_Date { get; set; }
        public string Residual_value { get; set; }
        public string Depreciation { get; set; }
        public string Invoice_No { get; set; }
        public string Make { get; set; }
        public string Department { get; set; }
        public string Remark { get; set; }
        public string FileName { get; set; }
    }
}
