using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.ViewModels
{
    public class RoleVM
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
