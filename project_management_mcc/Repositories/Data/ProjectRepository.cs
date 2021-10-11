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
    public class ProjectRepository : GeneralRepository<MyContext, Project, int>
    {

        private readonly MyContext myContext;

        public ProjectRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int CreateProject(CreateProjectVM createProjectVM)
        {
            var stringHtmlMessage = "<h1>Notification Activity Assignment</h1>";

            var project = new Project()
            {
                ManagerId = createProjectVM.ManagerId,
                Name = createProjectVM.ProjectName,
                Description = createProjectVM.Description
            };

            // save to db
            myContext.Projects.Add(project);
            myContext.SaveChanges();

            if(createProjectVM.ActivityVMs != null)
            {
                foreach (var item in createProjectVM.ActivityVMs)
                {
                    var activity = new Activity()
                    {
                        ProjectId = project.Id,
                        Name = item.ActivityName,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        status = (Activity.Status)item.status
                    };
                    //save to db
                    myContext.Activities.Add(activity);
                    myContext.SaveChanges();

                    if (item.CreateAssignEmployeeVMs != null)
                    {
                        foreach (var x in item.CreateAssignEmployeeVMs)
                        {
                            var employeeActivity = new EmployeeActivity()
                            {
                                ActivityId = activity.Id,
                                EmployeeId = x.EmployeeId
                            };
                            //save to db
                            myContext.EmployeeActivities.Add(employeeActivity);

                            // ambil email dari create assign vm
                            MailHandler.Email(stringHtmlMessage, x.Email, subjectMail: "Activity Assignment");
                        }
                    } 
                    else
                    {
                        return myContext.SaveChanges();
                    }
                }
            }
            else
            {
                return myContext.SaveChanges();
            }
            return myContext.SaveChanges();
        }
        public int CloseProject(UpdateStatusVM updateStatusVM)
        {
            var closeProject = myContext.Projects.Where(x => x.Id == updateStatusVM.Id).FirstOrDefault();
            closeProject.status = (Project.Status)updateStatusVM.status;

            myContext.Projects.Update(closeProject);
            return myContext.SaveChanges();
        }
        public IEnumerable<Project> GetCreateProjectVMs(int Id)
        {
            var getcreateprojectId = myContext.Projects.Where(x => x.ManagerId == Id);
            //var getCreateProjectId = (from e in myContext.Employees
            //                          join p in myContext.Projects on e.ManagerId equals p.Id
            //                          select new CreateProjectVM
            //                          {
            //                              ManagerId = (int)e.ManagerId,
            //                              ProjectName = p.Name,
            //                              Description = p.Description
            //                          }).ToList();
            if (getcreateprojectId == null)
            {
                return null;
            }
            return getcreateprojectId;
        }
    }
}
