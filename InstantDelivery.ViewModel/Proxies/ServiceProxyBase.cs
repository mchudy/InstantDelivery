using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    //TODO: proper error handling
    //TODO: not sure if that's a right way to do this, it seems to work for now
    public abstract class ServiceProxyBase
    {
        private readonly string controllerName;
        //TODO: move to configuration
        private static readonly Uri baseUri = new Uri("http://localhost:13600/api/");

        protected static HttpClient client;

        protected ServiceProxyBase(string controllerName)
        {
            this.controllerName = controllerName;
        }

        public async Task<TResult> Get<TResult>(string query)
        {
            var response = await client.GetAsync($"{controllerName}/{query}");
            //TODO: react according to the status code
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<TResult>();
        }

        public async Task Delete(int id)
        {
            var response = await client.DeleteAsync($"{controllerName}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Put<TRequest>(TRequest dto)
        {
            var response = await client.PutAsJsonAsync(controllerName, dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task PostAsJson<TRequest>(string query, TRequest dto)
        {
            var response = await client.PostAsJsonAsync($"{controllerName}/{query}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TResult> PostAsJson<TRequest, TResult>(string query, TRequest dto)
        {
            var response = await client.PostAsJsonAsync($"{controllerName}/{query}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<TResult>();
        }

        public async Task Post(string query, HttpContent content)
        {
            var response = await client.PostAsync($"{controllerName}/{query}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TResult> Post<TResult>(string query, HttpContent content)
        {
            var response = await client.PostAsync($"{controllerName}/{query}", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<TResult>();
        }

        public static bool Login(string username, string password)
        {
            var response = GetToken(username, password);
            dynamic responseJson = JObject.Parse(response);
            string token = responseJson["access_token"];
            if (token != null)
            {
                client = CreateClient(token);
                return true;
            }
            return false;
        }

        public static void Logout()
        {
            client?.Dispose();
            client = null;
        }

        private static string GetToken(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("grant_type", "password"),
                            new KeyValuePair<string, string>("username", userName ),
                            new KeyValuePair<string, string>("password", password )
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(baseUri + "Token", content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        private static HttpClient CreateClient(string accessToken)
        {
            var newClient = new HttpClient();
            newClient.BaseAddress = baseUri;
            newClient.DefaultRequestHeaders.Accept.Clear();
            newClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(accessToken))
            {
                newClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            return newClient;
        }
    }
}
