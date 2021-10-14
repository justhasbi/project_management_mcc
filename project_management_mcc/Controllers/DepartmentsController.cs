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
    public class DepartmentsController : BaseController<Department, DepartmentRepository, int>
    {
        private readonly DepartmentRepository repository;
        public DepartmentsController(DepartmentRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpGet("GetDepartmen")]
        public ActionResult GetDepartmens()
        {
            try
            {
                var data = repository.GetDepartmens();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
