using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{
    [Table("tb_m_activity_history")]
    public class ActivityHistory
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; }
        
        public DateTime Update_date { get; set; }

        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
