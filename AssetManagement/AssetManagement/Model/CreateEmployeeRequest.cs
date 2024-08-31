using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.Model
{
    public class CreateEmployeeRequest
    {
        public string Company_Id { get; set; }
        public string Emp_code { get; set; }
        public string Emp_Name { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Branch { get; set; }
        public string Contact { get; set; }
        public string EmailId { get; set; }
    }
}
