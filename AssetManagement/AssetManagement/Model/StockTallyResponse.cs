using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class StockTallyResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<STockTallyDetails> STockTallyDetails { get; set; }
    }
    public class STockTallyDetails
    {

        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string Asset_name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Branch { get; set; }
        public string Employee { get; set; }
        public string sDate { get; set; }
        public string Status { get; set; } = "Missing";
        public string User_Name { get; set; }
        public string SubCategory { get; set; }
    }
}
