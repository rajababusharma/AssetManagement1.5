using System;
using System.Collections.Generic;

namespace AssetManagement.Model
{
    public class BranchWiseAssets
    {
        public string Branch { get; set; }
        public int Asset_Count { get; set; } = 0;

        public static implicit operator List<object>(BranchWiseAssets v)
        {
            throw new NotImplementedException();
        }
    }
}