using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Base;
using project_management_mcc.Models;
using project_management_mcc.Repositories.Data;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace project_management_mcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseController<Activity, ActivityRepository, int>
    {

        private readonly ActivityRepository activityRepository;

        public ActivitiesController(ActivityRepository activityRepository) : base(activityRepository)
        {
            this.activityRepository = activityRepository;
        }

        [HttpPost("CreateMultipleActivity")]
        public ActionResult InsertMultipleActivity(CreateListActivityVM createListActivityVM)
        {
            try
            {
                activityRepository.CreateMultipleActivity(createListActivityVM);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    Message = "Success"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Message = "Failed"
                });
            }
        }
    }
}
