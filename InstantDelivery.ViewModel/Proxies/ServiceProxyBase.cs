using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InstantDelivery.ViewModel.Proxies
{
    //TODO: proper error handling
    public abstract class ServiceProxyBase
    {
        //TODO: move to configuration
        private readonly Uri baseUri = new Uri("http://localhost:13600/api/");
        private static string accessToken;

        protected HttpClient client = new HttpClient();

        protected ServiceProxyBase(string controllerName)
        {
            client.BaseAddress = new Uri(baseUri, controllerName);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public static bool Login(string username, string password)
        {
            var response = GetToken(username, password);
            dynamic responseJson = JObject.Parse(response);
            string token = responseJson["access_token"];
            if (token != null)
            {
                accessToken = token;
                return true;
            }
            return false;
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
            using (var client = new HttpClient())
            {
                var response = client.PostAsync("http://localhost:13600/api/Token", content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
