using project_management_mcc.Context;
using project_management_mcc.Helper;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int >
    {

        private readonly MyContext myContext;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var hashPassword = HashGenerator.HashPassword(registerVM.Password);

            var employee = new Employee()
            {
                // pk
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                gender = (Employee.Gender)registerVM.gender,
                Phone = registerVM.Phone,
                DepartmentId = registerVM.DepartmentId,
                JobId = registerVM.JobId
            };
            myContext.Employees.Add(employee);
            var insert = myContext.SaveChanges();

            var account = new Account()
            {
                Id = employee.Id,
                Email = registerVM.Email,
                Password = hashPassword
            };
            myContext.Accounts.Add(account);
            insert = myContext.SaveChanges();
            return insert;
        }
    }
}
