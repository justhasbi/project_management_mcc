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
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }
        [HttpGet("GetEmployee")]
        public ActionResult GetEmployees()
        {
            try
            {
                var data = employeeRepository.GetEmployees();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
