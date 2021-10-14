using Client.Base.URL;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories
{
    public class EmployeeActivityRepository : GeneralRepository<EmployeeActivity, int>
    {
        private readonly Address address;

        private readonly HttpClient httpClient;

        private readonly string request;

        private readonly IHttpContextAccessor contextAccessor;

        public EmployeeActivityRepository(Address address, string request = "EmployeeActivities/") : base(address, request)
        {
            this.address = address;

            this.request = request;
            
            contextAccessor = new HttpContextAccessor();
            
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<List<EmployeeActivityVM>> GetEmployeeActivity(int id)
        {
            List<EmployeeActivityVM> entities = new List<EmployeeActivityVM>();

            using (var response = await httpClient.GetAsync(request + "GetEmployeeActivity/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<EmployeeActivityVM>>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode DeleteEmployeeAssignment(EmployeeActivityVM employeeActivityVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(employeeActivityVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "DeleteEmployeeAssignment/", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode AssignMultipleEmployee(CreateListAssignEmployeeVM createListAssignEmployeeVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(createListAssignEmployeeVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "AssignMultipleEmployee/", content).Result;
            return result.StatusCode;
        }
    }
}
