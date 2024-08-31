using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class RecoverPasswordResp
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public RecoverPassword recoverPassword { get; set; }
    }
    public class RecoverPassword
    {

        public string Emp_Name { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
    }
}
