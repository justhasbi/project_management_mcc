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
        [Required(ErrorMessage = "Id is required")]
        [MaxLength(2, ErrorMessage = "Id must be between 1 and 2 characters"), MinLength(1)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Update_date { get; set; }

        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
