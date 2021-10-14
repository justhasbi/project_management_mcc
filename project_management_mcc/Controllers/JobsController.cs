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
    public class JobsController : BaseController<Job, JobRepository, int>
    {
        private readonly JobRepository repository;
        public JobsController(JobRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpGet("GetJobs")]
        public ActionResult GetJobs()
        {
            try
            {
                var data = repository.GetJobs();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
