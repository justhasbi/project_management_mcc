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
    public class AccountRepository : GeneralRepository<Account, int>
    {
        private readonly Address address;

        private readonly HttpClient httpClient;

        private readonly string request;

        private readonly IHttpContextAccessor contextAccessor;

        public AccountRepository(Address address, string request = "Accounts/") : base(address, request)
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

        public async Task<JwtTokenVM> Auth(LoginVM loginVM)
        {
            JwtTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync(request + "Login", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JwtTokenVM>(apiResponse);

            return token;
        }
    }
}
