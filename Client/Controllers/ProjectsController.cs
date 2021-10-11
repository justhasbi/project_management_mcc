using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project_management_mcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ProjectsController : BaseController<Project, ProjectRepository, int>
    {
        private readonly ProjectRepository repository;

        public ProjectsController(ProjectRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        

        public IActionResult Index()
        {
            var userIdentity = User.Identity.IsAuthenticated;

            if (userIdentity)
            {
                var payload = HttpContext.Session.GetString("Payload");
                var jsonPayload = JsonConvert.DeserializeObject(payload);
                ViewBag.Payload = jsonPayload;
                return View();
            }

            return RedirectToAction("Index", "Accounts");
        }

        public IActionResult ProjectDetail()
        {
            return View();
        }
    }
}
