using InstantDelivery.Model;
using InstantDelivery.Service.Paging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace InstantDelivery.Tests
{
    public class WebApiTests
    {
        [Fact]
        public async void CanConnectToService()
        {
            var query = new PageQuery<EmployeeDto>
            {
                PageSize = 10,
                PageIndex = 1,
                SortProperty = "FirstName"
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13600/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Employees/Page", query);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
