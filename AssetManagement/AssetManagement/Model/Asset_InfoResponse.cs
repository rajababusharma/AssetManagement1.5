using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class Asset_InfoResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public CreateAssetRequest Assets { get; set; }
    }
}
