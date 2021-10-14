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
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class JobRepository : GeneralRepository<Job, int>
    {
        private readonly Address address;

        private readonly HttpClient httpClient;

        private readonly string request;

        private readonly IHttpContextAccessor contextAccessor;

        public JobRepository(Address address, string request = "Job/") : base(address, request)
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
        public async Task<List<EmployeeVM>> GetJobs()
        {
            List<EmployeeVM> job = new List<EmployeeVM>();
            using (var respone = await httpClient.GetAsync(request + "GetJobs"))
            {
                string apiRespone = await respone.Content.ReadAsStringAsync();
                job = JsonConvert.DeserializeObject<List<EmployeeVM>>(apiRespone);
            }
            return job;
        }

    }
}
