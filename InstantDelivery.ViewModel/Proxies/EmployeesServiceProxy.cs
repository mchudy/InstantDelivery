using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Paging;
using InstantDelivery.ViewModel.Dialogs;
using InstantDelivery.ViewModel.Extensions;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class EmployeesServiceProxy : ServiceProxyBase
    {
        public EmployeesServiceProxy(IDialogManager dialogManager)
            : base("Employees", dialogManager)
        { }

        /// <summary>
        /// Zwraca dane pracownika o podanym ID.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeDto> GetById(int employeeId)
        {
            return await Get<EmployeeDto>(employeeId.ToString());
        }

        /// <summary>
        /// Zwraca dane zalogowanego pracownika.
        /// </summary>
        /// <returns></returns>
        public async Task<EmployeeDto> GetLoggedData()
        {
            const string queryString = "LoggedCourierData";
            return await Get<EmployeeDto>(queryString);
        }

        /// <summary>
        /// Zwraca stronę danych pracowników.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<EmployeeDto>> Page(PageQuery query)
        {
            return await Get<PagedResult<EmployeeDto>>("Page?" + query.ToQueryString());
        }

        /// <summary>
        /// Zwraca stronę danych pracowników z ich paczkami.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<EmployeePackagesDto>> PackagesPage(PageQuery query)
        {
            string queryString = "Packages/Page?" + query.ToQueryString();
            return await Get<PagedResult<EmployeePackagesDto>>(queryString);
        }

        /// <summary>
        /// Zwraca stronę danych pracowników z ich pojazdami.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<EmployeeVehicleDto>> VehiclesPage(PageQuery query)
        {
            string queryString = "Vehicles/Page?" + query.ToQueryString();
            return await Get<PagedResult<EmployeeVehicleDto>>(queryString);
        }

        /// <summary>
        /// Usuwa pracownika o danym ID.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task DeleteEmployee(int employeeId)
        {
            await Delete(employeeId);
        }

        /// <summary>
        /// Aktualizuje pracownika.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task UpdateEmployee(EmployeeDto employee)
        {
            await Put(employee);
        }

        /// <summary>
        /// Przypisuje inny pojazd do pracownika.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public async Task ChangeVehicle(int employeeId, int? vehicleId)
        {
            string queryString = $"ChangeVehicle?employeeid={employeeId}&vehicleId={vehicleId}";
            await Post(queryString, null);
        }

        /// <summary>
        /// Dodaje pracownika do bazy danych.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task AddEmployee(EmployeeAddDto employee)
        {
            await PostAsJson("", employee);
        }
    }
}
