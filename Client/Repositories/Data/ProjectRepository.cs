using Client.Base.URL;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using project_management_mcc.Models;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class ProjectRepository : GeneralRepository<Project, int>
    {
        private readonly Address address;

        private readonly HttpClient httpClient;
        
        private readonly string request;

        private readonly IHttpContextAccessor contextAccessor;
        
        public ProjectRepository(Address address, string request = "Projects/") : base(address, request)
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

        // get project by manager id
        public async Task<List<Project>> GetManagerId(int id)
        {
            List<Project> entities = new List<Project>();

            using (var response = await httpClient.GetAsync(request + "GetManagerId/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Project>>(apiResponse);
            }
            return entities;
        }

        public string CloseProject(UpdateStatusVM updateStatusVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(updateStatusVM), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "CloseProject", content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string ProjectUpdate(Project project)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request, content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        public async Task<List<GetProjectByEmployeeActVM>> GetProjectByEmployeeActivity(int id)
        {
            List<GetProjectByEmployeeActVM> entities = new List<GetProjectByEmployeeActVM>();

            using (var response = await httpClient.GetAsync(request + "GetProjectEmployee/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<GetProjectByEmployeeActVM>>(apiResponse);
            }
            return entities;
        }
    }
}
