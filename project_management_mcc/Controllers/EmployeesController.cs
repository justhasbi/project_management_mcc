using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Base;
using project_management_mcc.Models;
using project_management_mcc.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, int>
    {

        private readonly EmployeeRepository repository;

        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetEmployees")]
        public ActionResult GetEmployees()
        {
            try
            {
                var data = repository.GetEmployee();
                return Ok(data);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
    }
}
