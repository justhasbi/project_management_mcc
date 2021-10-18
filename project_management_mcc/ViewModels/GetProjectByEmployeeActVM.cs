using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.ViewModels
{
    public class GetProjectByEmployeeActVM
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public enum Status
        {
            Unstarted,
            Started,
            Completed
        }

        [Range(0, 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status status { get; set; }
    }
}
