using Client.Base.URL;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, int>
    {
        private readonly Address address;

        private readonly HttpClient httpClient;

        private readonly string request;

        private readonly IHttpContextAccessor contextAccessor;

        public EmployeeRepository(Address address, string request = "Employees/") : base (address, request)
        {
            this.address = address;
            this.request = request;
            contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", contextAccessor.HttpContext.Session.GetString("JWToken"));
        }
        public async Task<List<EmployeeVM>> GetEmployees()
        {
            List<EmployeeVM> employees = new List<EmployeeVM>();
            using (var respone = await httpClient.GetAsync(request + "GetEmployees"))
            {
                string apiRespone = await respone.Content.ReadAsStringAsync();
                employees = JsonConvert.DeserializeObject<List<EmployeeVM>>(apiRespone);
            }
            return employees;
        }
    }
}
