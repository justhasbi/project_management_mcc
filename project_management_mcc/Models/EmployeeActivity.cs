using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Models
{
    [Table("tb_tr_employee_activity")]
    public class EmployeeActivity
    {
        [Key]
        public int Id { get; set; }

        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
