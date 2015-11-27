using InstantDelivery.Model;
using InstantDelivery.Services.Paging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class EmployeesServiceProxy
    {
        private HttpClient client = new HttpClient();

        public EmployeesServiceProxy()
        {
            client.BaseAddress = new Uri("http://localhost:13600/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PagedResult<EmployeeDto>> Page(PageQuery<EmployeeDto> query)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Employees/Page", query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PagedResult<EmployeeDto>>();
        }
    }
}
