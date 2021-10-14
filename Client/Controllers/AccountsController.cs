using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System.IdentityModel.Tokens.Jwt;
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

            if (token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);

            var handler = new JwtSecurityTokenHandler();
            var tokenDecode = handler.ReadJwtToken(token);
            var jwtPayloadDecode = tokenDecode.Payload.SerializeToJson();

            HttpContext.Session.SetString("Payload", jwtPayloadDecode);

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

        [HttpPut]
        public JsonResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var result = repository.ChangePassword(changePasswordVM);
            return Json(result);
        }

        [Authorize]
        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // view handler
        public IActionResult Index()
        {
            var userIdentity = User.Identity.IsAuthenticated;

            if (userIdentity)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // sample
        public IActionResult RegisterViews()
        {
            var userIdentity = User.Identity.IsAuthenticated;

            if (userIdentity)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult ForgotPasswordViews()
        {
            var userIdentity = User.Identity.IsAuthenticated;
            
            if (userIdentity)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult ChangePasswordViews()
        {
            var userIdentity = User.Identity.IsAuthenticated;
            
            if (!userIdentity)
            {
                return RedirectToAction("Index");
            }
            
            var payload = HttpContext.Session.GetString("Payload");
            var jsonPayload = JsonConvert.DeserializeObject(payload);
            ViewBag.Payload = jsonPayload;

            return View();

        }
    }
}
