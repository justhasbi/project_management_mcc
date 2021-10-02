using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Base;
using project_management_mcc.Models;
using project_management_mcc.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityHistoriesController : BaseController<ActivityHistory, ActivityHistoryRepository, int>
    {
        public ActivityHistoriesController(ActivityHistoryRepository repository) : base(repository)
        {
        }
    }
}
