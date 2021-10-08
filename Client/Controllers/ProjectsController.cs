using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Base;
using project_management_mcc.Models;
using project_management_mcc.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ProjectsController : BaseController<Project, ProjectRepository, int>
    {
        public readonly ProjectRepository repository;
        public ProjectsController (ProjectRepository repository) : base(repository)
        {
            this.repository = repository;
        }


    }
}
