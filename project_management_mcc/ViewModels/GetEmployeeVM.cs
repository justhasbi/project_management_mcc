using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.ViewModels
{
    public class GetEmployeeVM
    {
        public int EmployeeId { get; set; }

        public string Fullname { get; set; }

        public string DepartmentName { get; set; }

        public string JobName { get; set; }

        public string Email { get; set; }

        public int? ActivityId { get; set; }
        
        public int RoleId { get; set; }
        
        public string RoleName { get; set; }
    }
}
