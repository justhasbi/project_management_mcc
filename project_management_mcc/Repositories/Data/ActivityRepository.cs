using project_management_mcc.Context;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories.Data
{
    public class ActivityRepository : GeneralRepository<MyContext, Activity, int>
    {

        private readonly MyContext myContext;

        public ActivityRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        // insert multiple activity
        public int CreateMultipleActivity(CreateListActivityVM createListActivityVM)
        {
            foreach (var item in createListActivityVM.CreateActivityVMs)
            {
                var activity = new Activity()
                {
                    Name = item.ActivityName,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    status = (Activity.Status)item.status,
                    ProjectId = (int)item.ProjectId
                };
                myContext.Activities.Add(activity);
            }
            return myContext.SaveChanges();
        }

        // update activity status
        public int UpdateActivityStatus(UpdateStatusVM updateStatusVM)
        {
            // cari activity yang ingin diubah
            var activity = myContext.Activities.Where(x => x.Id == updateStatusVM.Id).FirstOrDefault();
            activity.status = (Activity.Status)updateStatusVM.status;

            var actHistory = new ActivityHistory()
            {
                status = (ActivityHistory.Status)updateStatusVM.status,
                Update_date = DateTime.Now,  // update date
                ActivityId = updateStatusVM.Id  // activity ID
            };

            myContext.Activities.Update(activity);
            return myContext.SaveChanges();

        }
    }
}
