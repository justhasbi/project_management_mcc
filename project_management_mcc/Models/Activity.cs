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
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
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
