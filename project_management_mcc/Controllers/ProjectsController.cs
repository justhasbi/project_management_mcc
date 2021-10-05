﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
