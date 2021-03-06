using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        public enum Status
        {
            Unstarted,
            Started,
            Completed
        }

        [Range(0, 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Update_date { get; set; }

        public int ActivityId { get; set; }
        [JsonIgnore]
        public virtual Activity Activity { get; set; }
    }
}
