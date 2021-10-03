using project_management_mcc.Context;
using project_management_mcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories.Data
{
    public class JobRepository : GeneralRepository<MyContext, Job, int>
    {
        public JobRepository(MyContext context) : base(context)
        {
        }
    }
}
