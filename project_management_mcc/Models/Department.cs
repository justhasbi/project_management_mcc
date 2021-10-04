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
        [Required(ErrorMessage = "Id is required")]
        [MaxLength(4, ErrorMessage = "Id must be between 2 and 4 characters"), MinLength(2)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, ErrorMessage = "Name must be between 5 and 40 characters", MinimumLength = 5)]
        public int Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
