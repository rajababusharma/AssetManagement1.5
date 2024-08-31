using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class DisposeReportResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<DisposalReportList> DisposalReportList { get; set; }
    }
    public class DisposalReportList
    {
        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string Asset_name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Branch { get; set; }
        public string ReasonFor_Dispose { get; set; }
        public string ModeOf_Disposal { get; set; }
        public string Residual_Value { get; set; }
        public string Authorized_By { get; set; }
        public string FinalLocation { get; set; }
        public string AssetDispose_Date { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }


    }
}
