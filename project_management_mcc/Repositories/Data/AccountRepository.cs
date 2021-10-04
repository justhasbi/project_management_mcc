using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using project_management_mcc.Context;
using project_management_mcc.Helper;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int >
    {

        private readonly MyContext myContext;
        public IConfiguration _configuration;

        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            _configuration = configuration;
        }

        public int Register(RegisterVM registerVM)
        {
            var hashPassword = HashGenerator.HashPassword(registerVM.Password);

            var employee = new Employee()
            {
                // pk
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                gender = (Employee.Gender)registerVM.gender,
                Phone = registerVM.Phone,
                DepartmentId = registerVM.DepartmentId,
                JobId = registerVM.JobId
            };
            myContext.Employees.Add(employee);
            var insert = myContext.SaveChanges();

            var account = new Account()
            {
                Id = employee.Id,
                Email = registerVM.Email,
                Password = hashPassword
            };
            myContext.Accounts.Add(account);
            insert = myContext.SaveChanges();
            return insert;
        }
        public JwtTokenVM Login(LoginVM loginVM)
        {
            var emailCheck = myContext.Accounts.Where(x => x.Email.Equals(loginVM.Email)).FirstOrDefault();
            if (emailCheck == null)
            {
                return new JwtTokenVM
                {
                    Messages = "Wrong Email!",
                    Token = null
                };
            }
            else
            {
                var passwordCheck = myContext.Accounts.Where(x => x.Password.Equals(loginVM.Password)).FirstOrDefault();
                var validatePassword = HashGenerator.ValidatePassword(loginVM.Password, passwordCheck.Password);
                if (emailCheck != null)
                {
                    if (validatePassword == false)
                    {
                        return new JwtTokenVM
                        {
                            Messages = "Wrong Password!",
                            Token = null
                        };
                    }
                    else
                    {
                        var getRole = (from e in myContext.Employees
                                       join a in myContext.Accounts on e.Id equals a.Id
                                       join ar in myContext.AccountRoles on a.Id equals ar.AccountId
                                       join r in myContext.Roles on ar.RoleId equals r.Id
                                       select new RoleVM
                                       {
                                           Id = ar.Id,
                                           RoleId = ar.RoleId,
                                           RoleName = r.Name
                                       }).Where(x => x.Id.Equals(emailCheck.Id)).ToList();
                        var claims = new List<Claim>
                        {
                            new Claim("Id", passwordCheck.Id.ToString()),
                            new Claim(ClaimTypes.Email, emailCheck.Email)
                        };
                        foreach (var item in getRole)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item.RoleName));
                        }
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                        );
                        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                        return new JwtTokenVM
                        {
                            Messages = "Succes Login!",
                            Token = jwtToken
                        };
                    }
                }
                return new JwtTokenVM
                {
                    Messages = "Wrong Email!"
                };
            }
        }
    }
}
