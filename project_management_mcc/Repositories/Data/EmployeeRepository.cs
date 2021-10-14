using project_management_mcc.Context;
using project_management_mcc.Helper;
using project_management_mcc.Models;
using project_management_mcc.Repositories;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, int>
    {

        private readonly MyContext myContext;

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        
        public IEnumerable<GetEmployeeVM> GetEmployeeJob()
        {
            var data = (
                from e in myContext.Employees
                join a in myContext.Accounts on e.Id equals a.Id
                join ar in myContext.AccountRoles on a.Id equals ar.AccountId
                join r in myContext.Roles on ar.RoleId equals r.Id
                join j in myContext.Jobs on e.JobId equals j.Id
                join d in myContext.Departments on j.DepartmentId equals d.Id
                select new GetEmployeeVM
                {
                    EmployeeId = e.Id,
                    Fullname = $"{e.FirstName} {e.LastName}",
                    DepartmentName = d.Name,
                    JobName = j.Name,
                    Email = a.Email,
                    RoleId = r.Id,
                    RoleName = r.Name
                }).ToList();
                
            return data;
        }
        
        public IEnumerable<EmployeeVM> GetEmployees()
        {
            var data = (from e in myContext.Employees
                        join a in myContext.Accounts on e.Id equals a.Id
                        select new EmployeeVM
                        {
                            EmployeeId = e.Id,
                            FullName = $"{e.FirstName} {e.LastName}",
                            Phone = e.Phone,
                            gender = (EmployeeVM.Gender)e.gender,
                            Email = a.Email
                        }).ToList();
            return data;
        }
    }
}
