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
    public class RoleRepository : GeneralRepository<Role, int>
    {
        private readonly Address address;

        private readonly HttpClient httpClient;

        private readonly string request;

        private readonly IHttpContextAccessor contextAccessor;

        public RoleRepository (Address address, string request = "Roles/") : base(address, request)
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
        public async Task<List<RoleVM>> GetRoles()
        {
            List<RoleVM> roles = new List<RoleVM>();
            using (var respone = await httpClient.GetAsync(request + "GetRoles"))
            {
                string apiRespone = await respone.Content.ReadAsStringAsync();
                roles = JsonConvert.DeserializeObject<List<RoleVM>>(apiRespone);
            }
            return roles;
        }
    }
}
