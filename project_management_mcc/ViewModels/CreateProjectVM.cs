using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.ViewModels
{
    public class CreateProjectVM
    {
        public int ManagerId { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public List<CreateActivityVM> ActivityVMs { get; set; }
    }
}
