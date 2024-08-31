using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class AMCDetailsResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<AssetAMCList> AssetAMCList { get; set; }
    }
	public class AssetAMCList
	{
		public string Company_Id { get; set; }
		public string Asset_id { get; set; }
		public string Asset_Name { get; set; }
		public string Vendor_Name { get; set; }
		public string AMC_Type { get; set; }
		public string AMC_StartDate { get; set; }
		public string AMC_EndDate { get; set; }
		public string AMC_AlertDate { get; set; }
		public string AMC_Description { get; set; }
		public string AMC_Value { get; set; }
		public string Email_Id { get; set; }
		public string Image1 { get; set; }

	}
}
