using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{
    [Table("tb_m_employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        public enum Gender
        {
            Male,
            Female
        }

        [Required]
        public Gender gender { get; set; }
        
        // self referencing ManagerID
        public int ManagerId { get; set; }
        public virtual Employee EmployeeParent { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        // department_id
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        // job_id
        public int JobId { get; set; }
        public virtual Job Job { get; set; }

        // project_id
        public virtual ICollection<Project> Projects { get; set; }

        // employee activity
        public virtual ICollection<EmployeeActivity> EmployeeActivities { get; set; }

        public virtual Account Account { get; set; }
    }
}
