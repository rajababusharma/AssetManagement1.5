using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class CreateSubCategoryRequest
    {
        public string Asset_Code { get; set; }
        public string Category_Description { get; set; }
        public string SubCategory_Description { get; set; }
    }
}
