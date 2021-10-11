using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ActivitiesController : BaseController<Activity, ActivityRepository, int>
    {
        private readonly ActivityRepository repository;

        public ActivitiesController(ActivityRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetByProjectId(int id)
        {
            var result = await repository.GetByProjectId(id);
            return Json(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
