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

        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        [StringLength(40, ErrorMessage = "First name must be between 3 and 40 characters", MinimumLength = 3)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name is required")]
        [DataType(DataType.Text)]
        [StringLength(40, ErrorMessage = "Last name must be between 3 and 40 characters", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(12, ErrorMessage = "Phone must be 12 characters"), MinLength(12)]
        public string Phone { get; set; }

        public enum Gender
        {
            Male,
            Female
        }

        [Required]
        public Gender gender { get; set; }
        
        // self referencing ManagerID
        [ForeignKey("ManagerId")]
        public int? ManagerId { get; set; }
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
