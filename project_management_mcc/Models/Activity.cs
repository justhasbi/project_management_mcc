using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{
    [Table("tb_m_activity")]
    public class Activity
    {

        [Key]
        [Required(ErrorMessage = "Id is required")]
        [MaxLength(2, ErrorMessage = "Id must be between 1 and 2 characters"), MinLength(1)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, ErrorMessage = "Name must be between 5 and 40 characters", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public enum Status
        {
            Unstarted,
            Started,
            Completed
        }
        [Required]
        public Status status { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        // employee activity
        public virtual ICollection<EmployeeActivity> EmployeeActivities { get; set; }

        // activity history
        public virtual ICollection<ActivityHistory> ActivityHistories { get; set; }
    }
}
