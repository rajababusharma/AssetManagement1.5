using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class GetAMC_RepairCount
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<GetAssetTypeCount> GetAssetTypeCount { get; set; }
    }
}
