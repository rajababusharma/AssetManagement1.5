using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class AssetDataResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<CreateAssetRequest> Assets { get; set; }
    }
}
