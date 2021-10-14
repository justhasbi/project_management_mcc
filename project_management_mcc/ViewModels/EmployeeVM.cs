using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.ViewModels
{
    public class EmployeeVM
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        public Gender gender { get; set; }
        public string Email { get; set; }
        public string ActivityName { get; set; }
        public string ProjectName { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public int DepartemenId { get; set; }
        public string DepartmenName { get; set; }
    }
}
