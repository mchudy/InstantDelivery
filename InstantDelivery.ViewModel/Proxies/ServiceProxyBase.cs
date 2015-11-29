using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InstantDelivery.ViewModel.Proxies
{
    //TODO: proper error handling
    public abstract class ServiceProxyBase
    {
        //TODO: move to configuration
        private readonly Uri baseUri = new Uri("http://localhost:13600/api/");

        protected HttpClient client = new HttpClient();

        protected ServiceProxyBase(string controllerName)
        {
            client.BaseAddress = new Uri(baseUri, controllerName);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
