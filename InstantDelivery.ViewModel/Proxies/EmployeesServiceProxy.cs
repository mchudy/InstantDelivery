using InstantDelivery.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InstantDelivery.ViewModel.Extensions;

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

        public async Task<PagedResult<EmployeeDto>> Page(PageQuery query)
        {
            HttpResponseMessage response = await client.GetAsync("Page?" + query.ToQueryString());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PagedResult<EmployeeDto>>();
        }

        public async Task<PagedResult<EmployeePackagesDto>> PackagesPage(PageQuery query)
        {
            HttpResponseMessage response = await client.GetAsync("Packages/Page?" + query.ToQueryString());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PagedResult<EmployeePackagesDto>>();
        }

        public async Task<PagedResult<EmployeeVehicleDto>> VehiclesPage(PageQuery query)
        {
            HttpResponseMessage response = await client.GetAsync("Vehicles/Page?" + query.ToQueryString());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PagedResult<EmployeeVehicleDto>>();
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
