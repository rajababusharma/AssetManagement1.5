using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class BranchWiseAssetCount
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<BranchWiseAssets> BranchWiseAssets { get; set; }
    }
}
