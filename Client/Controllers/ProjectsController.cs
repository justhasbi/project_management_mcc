using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
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

        // get project by manager ID
        [HttpGet]
        public async Task<JsonResult> GetManagerId(int id)
        {
            var result = await repository.GetManagerId(id);
            return Json(result);
        }

        [HttpPut]
        public JsonResult CloseProject(UpdateStatusVM updateStatusVM)
        {
            var result = repository.CloseProject(updateStatusVM);
            return Json(result);
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
            var userIdentity = User.Identity.IsAuthenticated;

            if (userIdentity)
            {
                return View();
            }

            return RedirectToAction("Index", "Accounts");
        }
    }
}
