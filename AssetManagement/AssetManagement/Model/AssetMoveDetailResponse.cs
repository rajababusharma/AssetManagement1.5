using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class AssetMoveDetailResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<AssetMoveList> AssetMoveList { get; set; }
        public List<AMCList> AMCLists { get; set; }
        public List<InsuranceList> insuranceLists { get; set; }
    }
    public class AMCList
    {
        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string Asset_Name { get; set; }
        public string Branch { get; set; }
        public string Vendor_Name { get; set; }
        public string AMC_Type { get; set; }
        public DateTime AMC_StartDate { get; set; }
        public DateTime AMC_EndDate { get; set; }
        public DateTime AMC_AlertDate { get; set; }
        public string AMC_Description { get; set; }
        public string AMC_Value { get; set; }
        public string Image1 { get; set; }
    }
    public class InsuranceList
    {
        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string Asset_Name { get; set; }
        public string Branch { get; set; }
        public DateTime Policy_Date { get; set; }
        public string InsuranceCompany_Name { get; set; }
        public string Policy_No { get; set; }
        public DateTime Alert_Date { get; set; }
        public string Policy_Name { get; set; }
        public string Premium { get; set; }
        public DateTime Due_Date { get; set; }
        public string ModeOFPayment { get; set; }
        public string Image1 { get; set; }
    }

 

        public class AssetMoveList
    {
        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string Asset_name { get; set; }
        public string From_Location_Description { get; set; }
        public string To_Location_Description { get; set; }
        public string From_Floor { get; set; }
        public string To_Floor { get; set; }
        public string From_FinalLocation { get; set; }
        public string To_FinalLocation { get; set; }
        public string Employee { get; set; }
        public string AssetMove_Date { get; set; }
        public bool Status { get; set; } = false;
        public string Remarks { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
    }
}
