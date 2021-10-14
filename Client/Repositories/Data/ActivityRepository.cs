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
    public class ActivityRepository : GeneralRepository<Activity, int>
    {

        private readonly Address address;

        private readonly HttpClient httpClient;

        private readonly string request;

        private readonly IHttpContextAccessor contextAccessor;

        public ActivityRepository(Address address, string request = "Activities/") : base(address, request)
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

        public async Task<List<Activity>> GetByProjectId(int id)
        {
            List<Activity> entities = new List<Activity>();

            using (var response = await httpClient.GetAsync(request + "GetByProject/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Activity>>(apiResponse);
            }
            return entities;
        }

        public string UpdateActivityStatus(UpdateStatusVM updateStatusVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(updateStatusVM), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "UpdateActivityStatus", content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string UpdateActivity(Activity activity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(activity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request, content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
