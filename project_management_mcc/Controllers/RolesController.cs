using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Roles = "Manager, Human Resource")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        private readonly RoleRepository roleRepository;
        public RolesController(RoleRepository repository) : base(repository)
        {
            this.roleRepository = repository;
        }
        [HttpGet("GetRoles")]
        public ActionResult GetRoles()
        {
            try
            {
                var data = roleRepository.GetRoles();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
