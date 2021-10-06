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
    public class EmployeeActivityRepository : GeneralRepository<MyContext, EmployeeActivity, int>
    {

        private readonly MyContext myContext;

        public EmployeeActivityRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int AssignMultipleEmployee(CreateListAssignEmployeeVM createListAssignEmployeeVM)
        {

            var stringHtmlMessage = "<h1>Notification Activity Assignment</h1>";

            foreach (var item in createListAssignEmployeeVM.createAssignEmployeeVMs)
            {
                var employeeActivity = new EmployeeActivity()
                {
                    ActivityId = (int)item.ActivityId,
                    EmployeeId = item.EmployeeId
                };
                myContext.EmployeeActivities.Add(employeeActivity);
                MailHandler.Email(stringHtmlMessage, item.Email);
            }
            return myContext.SaveChanges();
        }
    }
}
