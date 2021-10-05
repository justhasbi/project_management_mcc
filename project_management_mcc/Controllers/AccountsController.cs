using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Base;
using project_management_mcc.Context;
using project_management_mcc.Models;
using project_management_mcc.Repositories.Data;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace project_management_mcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {

        private readonly AccountRepository accountRepository;
        private readonly MyContext myContext;

        public AccountsController(AccountRepository accountRepository, MyContext myContext) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.myContext = myContext;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {

            var checkEmail = myContext.Accounts.Where(x => x.Email.Equals(registerVM.Email)).FirstOrDefault();
            var checkPhone = myContext.Employees.Where(x => x.Phone.Equals(registerVM.Phone)).FirstOrDefault();

            if(checkEmail == null && checkPhone == null)
            {
                var registerResponse = accountRepository.Register(registerVM);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = registerVM,
                    message = "Success Register Employee"
                });
            } 
            else if (checkEmail != null)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email is already used"
                });
            }
            else if (checkPhone != null)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Phone number is already used"
                });

            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
            });
        }
        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var loginAction = accountRepository.Login(loginVM);

            return Ok(loginAction);
        }

    }
}
