using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class PostDisposeAssetRequest
    {
        public string Company_Id { get; set; }
        public string FinancialYear { get; set; }
        public string Asset_id { get; set; }
        public string Asset_name { get; set; }
        public string Description { get; set; }
        public string ReasonFor_Dispose { get; set; }
        public string ModeOf_Disposal { get; set; }
        public string Residual_Value { get; set; }
        public string Authorized_By { get; set; }
        public string Location { get; set; }
        public string Branch { get; set; }
        public string AssetDispose_Date { get; set; }
        public string Remarks { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
    }
}
