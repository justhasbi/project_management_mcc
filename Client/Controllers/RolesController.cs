
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
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        private readonly RoleRepository repository;

        public RolesController(RoleRepository repository): base(repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<JsonResult> GetRole()
        {
            var result = await repository.GetRoles();
            return Json(result);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
