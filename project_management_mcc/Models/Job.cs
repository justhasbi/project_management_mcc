using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{
    [Table("tb_m_job")]
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Job is required")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Name must be between 5 and 50 characters", MinimumLength = 5)]
        public string Name { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
