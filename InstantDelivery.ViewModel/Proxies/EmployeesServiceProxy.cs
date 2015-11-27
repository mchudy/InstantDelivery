using InstantDelivery.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    //TODO: proper error handling
    public class EmployeesServiceProxy
    {
        private HttpClient client = new HttpClient();

        public EmployeesServiceProxy()
        {
            client.BaseAddress = new Uri("http://localhost:13600/api/Employees/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PagedResult<EmployeeDto>> Page(PageQuery<EmployeeDto> query)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("Page", query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PagedResult<EmployeeDto>>();
        }

        public async Task<PagedResult<EmployeePackagesDto>> PackagesPage(PageQuery<EmployeePackagesDto> query)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("Packages/Page", query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PagedResult<EmployeePackagesDto>>();
        }

        public async Task DeleteEmployee(int employeeId)
        {
            HttpResponseMessage response = await client.DeleteAsync(employeeId.ToString());
            response.EnsureSuccessStatusCode();
        }

        public async Task<EmployeeDto> GetById(int employeeId)
        {
            var response = await client.GetAsync(employeeId.ToString());
            return await response.Content.ReadAsAsync<EmployeeDto>();
        }

        public async Task UpdateEmployee(EmployeeDto employee)
        {
            var response = await client.PutAsJsonAsync("", employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddEmployee(EmployeeAddDto employee)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("", employee);
            response.EnsureSuccessStatusCode();
        }
    }
}
