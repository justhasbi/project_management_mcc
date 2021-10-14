using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Base;
using project_management_mcc.Models;
using project_management_mcc.Repositories.Data;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Controllers
{
    // [Authorize(Roles = "Managers, HR")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController<Project, ProjectRepository, int>
    {

        private readonly ProjectRepository projectRepository;

        public ProjectsController(ProjectRepository repository) : base(repository)
        {
            this.projectRepository = repository;
        }

        [HttpPost("CreateProject")]
        public ActionResult CreateProject(CreateProjectVM createProjectVM)
        {
            try
            {
                var createProject = projectRepository.CreateProject(createProjectVM);
                return Ok(new
                {
                    message = "Success",
                });
            }
            catch
            {
                return BadRequest(new
                {
                    message = "Failed"
                });
            }
        }
        [HttpPut("CloseProject")]
        public ActionResult CloseProject(UpdateStatusVM updateStatusVM)
        {
            try
            {
                var closeProject = projectRepository.CloseProject(updateStatusVM);
                return Ok(new
                {
                    message = "Success"
                });
            }
            catch 
            {
                return BadRequest(new
                {
                    message = "Failed"
                });
            }
        }
        
        [HttpGet("GetManagerId/{id}")]
        public ActionResult GetManagerId(int id)
        {
            try
            {
                var manager = projectRepository.GetCreateProjectVMs(id);
                return Ok(manager);
            }
            catch
            {
                return BadRequest(new
                {
                    message = "Failed"
                });
            }
        }


    }
}
