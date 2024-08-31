using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class LoginResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public UserDetails UserDetails { get; set; }
    }
}
