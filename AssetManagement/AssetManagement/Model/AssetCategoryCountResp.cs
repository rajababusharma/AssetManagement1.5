using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class AssetCategoryCountResp
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<AssetCategoryCountList> AssetCategoryCountList { get; set; }
    }

    public class AssetCategoryCountList
    {
        public string AssetCategory { get; set; }
        public string Count { get; set; }
    }
}
