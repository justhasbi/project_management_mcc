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
    public class DepartmentsController : BaseController<Department, DepartmentRepository, int>
    {
        private readonly DepartmentRepository repository;
        public DepartmentsController(DepartmentRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpGet("GetDepartmen")]
        public async Task<JsonResult> GetDepaertment()
        {
            var result = await repository.GetDepartment();
            return Json(result);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
