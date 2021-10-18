using project_management_mcc.Context;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories.Data
{
    public class JobRepository : GeneralRepository<MyContext, Job, int>
    {
        private readonly MyContext myContext;
        public JobRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<EmployeeVM> GetJobs()
        {
            var data = (from e in myContext.Employees
                        join j in myContext.Jobs on e.JobId equals j.Id
                        select new EmployeeVM
                        {
                            EmployeeId = e.Id,
                            FullName = $"{e.FirstName} {e.LastName}",
                            JobId = j.Id,
                            JobName = j.Name,
                        }).ToList();
            return data;
        }
    }
}
