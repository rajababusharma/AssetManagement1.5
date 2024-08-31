using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class AssetsImagesResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<assetimages> Images { get; set; }
    }

    public class assetimages
    {
        public string ImageType { get; set; }
        public string Images { get; set; }
    }
}
