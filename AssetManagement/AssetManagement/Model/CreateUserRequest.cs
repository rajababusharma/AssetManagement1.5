using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class CreateUserRequest
    {
        public string Company_Id { get; set; }
        public string User_Code { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string IsActive { get; set; }
        public string Cdate { get; set; }
        public string Emp_Name { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Branch { get; set; }
        public string Contact { get; set; }
        public string EmailId { get; set; }
        public string pic { get; set; }
        public int User_Role { get; set; } = 0;
    }
}
