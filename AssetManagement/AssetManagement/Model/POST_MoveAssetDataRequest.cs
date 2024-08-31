using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{

    public class POST_MoveAssetDataRequest
    {
        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string Asset_name { get; set; }
        public string From_Location_Description { get; set; }
        public string To_Location_Description { get; set; }
        public string From_Floor { get; set; }
        public string To_Floor { get; set; }
        public string Employee { get; set; }
        public string AssetMove_Date { get; set; }
        public bool Status { get; set; } = false;
        public string Remarks { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
    }

   

   
}
