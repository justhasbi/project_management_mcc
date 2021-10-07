using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {

        private readonly AccountRepository repository;

        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("Auth/")]
        public async Task<ActionResult> Auth(LoginVM loginVM)
        {
            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.Token;

            if(token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public JsonResult Register(RegisterVM registerVM)
        {
            var result = repository.Register(registerVM);
            return Json(result);
        }

        [HttpPut]
        public JsonResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var result = repository.ForgotPassword(forgotPasswordVM);
            return Json(result);
        }

        // view handler
        public IActionResult Index()
        {
            return View();
        }

        // sample
        public IActionResult RegisterViews()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
