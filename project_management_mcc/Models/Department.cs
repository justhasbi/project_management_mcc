using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{
    [Table("tb_m_department")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, ErrorMessage = "Name must be between 5 and 40 characters", MinimumLength = 5)]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
