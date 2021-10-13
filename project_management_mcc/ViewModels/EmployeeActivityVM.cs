using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.ViewModels
{
    public class EmployeeActivityVM
    {
        public int EmployeeId { get; set; }



        public string Fullname { get; set; }

        public string DepartmentName { get; set; }

        public string JobName { get; set; }

        public string Email { get; set; }

        public int ActivityId { get; set; }
        
        public string ActivityName { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
