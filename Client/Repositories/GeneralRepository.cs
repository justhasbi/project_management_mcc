using Client.Base.URL;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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
    public class GeneralRepository<TEntity, XKey> : IRepository<TEntity, XKey>
        where TEntity : class
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public GeneralRepository(Address address, string request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public HttpStatusCode Delete(XKey key)
        {
            var result = httpClient.DeleteAsync(request + key).Result;
            return result.StatusCode;
        }

        public async Task<List<TEntity>> Get()
        {
            List<TEntity> entities = new List<TEntity>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TEntity>>(apiResponse);
            }
            return entities;
        }

        public async Task<TEntity> Get(XKey key)
        {
            TEntity entity = null;

            using (var response = await httpClient.GetAsync(request + key))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
            return entity;
        }

        public HttpStatusCode Put(XKey key, TEntity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + key, content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Post(TEntity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request, content).Result;
            return result.StatusCode;
        }
    }
}
