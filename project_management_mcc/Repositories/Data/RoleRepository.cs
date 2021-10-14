﻿using project_management_mcc.Context;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        private readonly MyContext myContext;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<RoleVM> GetRoles()
        {
            var data = (from e in myContext.Employees
                        join r in myContext.Roles on e.Id equals r.Id
                        select new RoleVM
                        {
                            EmployeeId = e.Id,
                            FullName = $"{e.FirstName} {e.LastName}",
                            RoleId = r.Id,
                            RoleName = r.Name
                        }).ToList();
            return data;
        }
    }
}
