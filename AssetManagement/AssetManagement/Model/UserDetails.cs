using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class UserDetails
    {
        public string Company_Id { get; set; }
        public string User_Code { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Branch { get; set; }
        public string Emp_Name { get; set; }
        public string User_Name { get; set; }
        public string Pic { get; set; }
        public bool Manage_Assets { get; set; }
        public bool Move_Asset { get; set; }
        public bool Transfer_Asset { get; set; }
        public bool Dispose_Asset { get; set; }
        public bool Repair_Asset { get; set; }
        public bool Rights_Managment { get; set; }
        public int User_Role { get; set; } = 0;
    }
}
