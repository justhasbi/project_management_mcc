using Client.Base.Controllers;
using Client.Repositories;
using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class EmployeeActivitiesController : BaseController<EmployeeActivity, EmployeeActivityRepository, int>
    {

        private readonly EmployeeActivityRepository repository;

        public EmployeeActivitiesController(EmployeeActivityRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetEmployeeActivity(int id)
        {
            var result = await repository.GetEmployeeActivity(id);
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteEmployeeAssignment([FromBody]EmployeeActivityVM employeeActivityVM)
        {
            var result = repository.DeleteEmployeeAssignment(employeeActivityVM);
            return Json(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
