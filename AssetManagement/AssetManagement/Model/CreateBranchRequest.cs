using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class CreateBranchRequest
    {
        public string Company_Id { get; set; }
        public string Location_Description { get; set; }
        public string Floor { get; set; }
    }
}
