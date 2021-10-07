using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace project_management_mcc.ViewModels
{
    public class CreateActivityVM
    {
        public int? ProjectId { get; set; }

        public string ActivityName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public enum Status
        {
            Unstarted,
            Started,
            Completed
        }

        [Range(0, 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status status { get; set; }

        public List<CreateAssignEmployeeVM> CreateAssignEmployeeVMs { get; set; }
    }
}
