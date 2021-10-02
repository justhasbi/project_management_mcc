using project_management_mcc.Context;
using project_management_mcc.Models;
using project_management_mcc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, int>
    {
        public EmployeeRepository(MyContext context) : base(context)
        {

        }
    }
}
