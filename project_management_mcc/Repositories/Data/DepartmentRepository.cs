using project_management_mcc.Context;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories.Data
{
    public class DepartmentRepository : GeneralRepository<MyContext, Department, int>
    {
        private readonly MyContext myContext;
        public DepartmentRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<EmployeeVM> GetDepartmens()
        {
            var data = (from e in myContext.Employees
                        join d in myContext.Departments on e.DepartmentId equals d.Id
                        select new EmployeeVM
                        {
                            EmployeeId = e.Id,
                            FullName = $"{e.FirstName} {e.LastName}",
                            DepartemenId = d.Id,
                            DepartmenName = d.Name
                        }).ToList();
            return data;
        }
    }
}
