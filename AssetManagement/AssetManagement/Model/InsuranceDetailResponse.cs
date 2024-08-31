using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class InsuranceDetailResponse
	{
		public string Status { get; set; }
		public string Msg { get; set; }
		public List<InsuranceDetailList> InsuranceDetailList { get; set; }
	}
    public class InsuranceDetailList
    {
        public string Company_Id { get; set; }
        public string Asset_id { get; set; }
        public string Policy_Name { get; set; }
        public string Maturity_Date { get; set; }
        public string Insurance_Date { get; set; }
        public string Policy_No { get; set; }
        public string InsuranceCompany_Name { get; set; }
        public string Policy_Date { get; set; }
        public string Premium { get; set; }
        public string Sum_Insured { get; set; }
        public string ModeOFPayment { get; set; }
        public string Image1 { get; set; }
    }
}

