using InstantDelivery.Model;
using InstantDelivery.ViewModel.Extensions;
using System.Threading.Tasks;
using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Paging;

namespace InstantDelivery.ViewModel.Proxies
{
    public class EmployeesServiceProxy : ServiceProxyBase
    {
        public EmployeesServiceProxy() : base("Employees")
        { }

        public async Task<EmployeeDto> GetById(int employeeId)
        {
            return await Get<EmployeeDto>(employeeId.ToString());
        }
        
        public async Task<EmployeeDto> GetLoggedData()
        {
            const string queryString = "LoggedCourierData";
            return await Get<EmployeeDto>(queryString);
        }

        public async Task<PagedResult<EmployeeDto>> Page(PageQuery query)
        {
            return await Get<PagedResult<EmployeeDto>>("Page?" + query.ToQueryString());
        }

        public async Task<PagedResult<EmployeePackagesDto>> PackagesPage(PageQuery query)
        {
            string queryString = "Packages/Page?" + query.ToQueryString();
            return await Get<PagedResult<EmployeePackagesDto>>(queryString);
        }

        public async Task<PagedResult<EmployeeVehicleDto>> VehiclesPage(PageQuery query)
        {
            string queryString = "Vehicles/Page?" + query.ToQueryString();
            return await Get<PagedResult<EmployeeVehicleDto>>(queryString);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await Delete(employeeId);
        }

        public async Task UpdateEmployee(EmployeeDto employee)
        {
            await Put(employee);
        }

        public async Task ChangeVehicle(int employeeId, int? vehicleId)
        {
            string queryString = $"ChangeVehicle?employeeid={employeeId}&vehicleId={vehicleId}";
            await Post(queryString, null);
        }

        public async Task AddEmployee(EmployeeAddDto employee)
        {
            await PostAsJson("", employee);
        }
    }
}
