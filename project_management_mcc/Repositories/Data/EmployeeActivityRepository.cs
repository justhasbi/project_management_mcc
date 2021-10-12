﻿using project_management_mcc.Context;
using project_management_mcc.Helper;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System.Collections.Generic;
using System.Linq;

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

            foreach (var item in createListAssignEmployeeVM.CreateAssignEmployeeVMs)
            {
                var employeeActivity = new EmployeeActivity()
                {
                    ActivityId = (int)item.ActivityId,
                    EmployeeId = item.EmployeeId
                };
                myContext.EmployeeActivities.Add(employeeActivity);

                MailHandler.Email(stringHtmlMessage, item.Email, subjectMail: "Activity Assignment");
            }
            return myContext.SaveChanges();
        }

        // get employee assign by activity id
        public IEnumerable<EmployeeActivityVM> GetEmployeeActivity(int id) // activity id
        {
            var data = (from a in myContext.Activities
                        join ea in myContext.EmployeeActivities on a.Id equals ea.ActivityId
                        join e in myContext.Employees on ea.EmployeeId equals e.Id
                        join j in myContext.Jobs on e.JobId equals j.Id
                        join d in myContext.Departments on j.DepartmentId equals d.Id
                        select new EmployeeActivityVM {
                            ActivityId = a.Id,
                            Fullname = $"{e.FirstName} {e.LastName}",
                            JobName = j.Name,
                            DepartmentName = d.Name
                        }).ToList();
            return data.Where(x => x.ActivityId.Equals(id));
        }
    }
}
