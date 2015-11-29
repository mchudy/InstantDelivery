using InstantDelivery.Model;
using InstantDelivery.ViewModel.Extensions;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class EmployeesServiceProxy : ServiceProxyBase
    {
        public EmployeesServiceProxy() : base("Employees")
        { }

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

        public async Task ChangeVehicle(int employeeId, int? vehicleId)
        {
            var response = await client.PostAsync(
                $"ChangeVehicle?employeeid={employeeId}&vehicleId={vehicleId}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddEmployee(EmployeeAddDto employee)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("", employee);
            response.EnsureSuccessStatusCode();
        }
    }
}
