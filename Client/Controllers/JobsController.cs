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
    public class JobsController : BaseController<Job, JobRepository, int>
    {
        private readonly JobRepository repository;

        public JobsController(JobRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpGet("GetJobs")]
        public async Task<JsonResult> GetJobs()
        {
            var result = await repository.GetJobs();
            return Json(result);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
