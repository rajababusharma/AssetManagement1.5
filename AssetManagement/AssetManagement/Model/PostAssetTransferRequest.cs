using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class PostAssetTransferRequest
    {
        public string Company_Id { get; set; }
        public string FinancialYear { get; set; }
        public string Asset_id { get; set; }
        public string Asset_name { get; set; }
        public string Description { get; set; }
        public string From_Employee { get; set; }
        public string To_Employee { get; set; }
        public string AssetTransfer_Date { get; set; }
       
    }
}
