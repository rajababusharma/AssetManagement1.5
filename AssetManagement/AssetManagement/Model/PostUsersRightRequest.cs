using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class PostUsersRightRequest
    {
        public string Company_Id { get; set; }
        public string User { get; set; }
        public bool Manage_Assets { get; set; }
        public bool Move_Asset { get; set; }
        public bool Transfer_Asset { get; set; }
        public bool Dispose_Asset { get; set; }
        public bool Repair_Asset { get; set; }
        public bool Rights_Managment { get; set; }
    }
}
