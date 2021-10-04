using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{
    [Table("tb_m_project")]
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Name must be between 5 and 50 characters", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Description must be between 5 and 500 characters", MinimumLength = 5)]
        public string Description { get; set; }

        // Manager ID
        public virtual int ManagerId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    
    }
}
