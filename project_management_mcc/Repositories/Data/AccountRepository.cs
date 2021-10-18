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
            
            var accountRoles = new AccountRole()
            {
                AccountId = account.Id,
                RoleId = (int)registerVM.RoleId
            };
            myContext.AccountRoles.Add(accountRoles);
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
                var passwordCheck = myContext.Accounts.Where(x => emailCheck.Id.Equals(x.Id)).FirstOrDefault();
                var getUserName = myContext.Employees.Where(x => passwordCheck.Id.Equals(x.Id)).FirstOrDefault();
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
                                           Id = a.Id,
                                           RoleId = ar.RoleId,
                                           RoleName = r.Name
                                       }).Where(x => x.Id == emailCheck.Id).ToList();
                        var claims = new List<Claim>
                        {
                            new Claim("Id", passwordCheck.Id.ToString()),
                            new Claim("Email", emailCheck.Email),
                            new Claim(ClaimTypes.Name, $"{getUserName.FirstName} {getUserName.LastName}")
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

        public void ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var emailCheck = myContext.Accounts.Where(x => x.Email.Equals(forgotPasswordVM.Email)).FirstOrDefault();

            if(emailCheck != null)
            {
                string guid = Guid.NewGuid().ToString();
                DateTime dateTime = DateTime.Now;
                string dateSend = dateTime.ToString("g");
                string stringHtmlMessage = $"Password diubah pada: {dateSend}\nPassword Baru Anda: {guid}";
                string hashPassword = HashGenerator.HashPassword(guid);
                var checkEmail = myContext.Accounts.Where(e => e.Id == emailCheck.Id).FirstOrDefault();
                checkEmail.Password = hashPassword;
                Update(checkEmail);

                MailHandler.Email(stringHtmlMessage, forgotPasswordVM.Email, subjectMail: "Reset Password");
            }
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var emailCheck = myContext.Accounts.Where(x => x.Email.Equals(changePasswordVM.Email)).FirstOrDefault();

            if (emailCheck == null)
            {
                // email belum terdaftar
                return 0;
            }
            else
            {
                var passwordCheck = myContext.Accounts.Where(x => emailCheck.Id.Equals(x.Id)).FirstOrDefault();
                var validatePassword = HashGenerator.ValidatePassword(changePasswordVM.CurrentPassword, passwordCheck.Password);
                if (emailCheck != null)
                {
                    if (validatePassword == false)
                    {
                        // password lama salah
                        return 1;
                    }
                    else
                    {
                        // sukses update password
                        var account = myContext.Accounts.Where(x => emailCheck.Id.Equals(x.Id)).FirstOrDefault();
                        passwordCheck.Password = HashGenerator.HashPassword(changePasswordVM.ConfirmPassword);
                        Update(account);
                        return 2;
                    }
                }
                // email belum terdaftar
                return 0;
            }
        }
    }
}
