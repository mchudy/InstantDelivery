using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Packages;
using InstantDelivery.Model.Paging;
using InstantDelivery.ViewModel.Dialogs;
using InstantDelivery.ViewModel.Extensions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace InstantDelivery.ViewModel.Proxies
{
    public class PackagesServiceProxy : ServiceProxyBase
    {
        public PackagesServiceProxy(IDialogManager dialogManager)
            : base("Packages", dialogManager)
        {
        }

        /// <summary>
        /// Zwraca stronę danych przesyłek.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<PackageDto>> Page(PageQuery query)
        {
            string queryString = "Employee/Page?" + query.ToQueryString();
            return await Get<PagedResult<PackageDto>>(queryString);
        }

        /// <summary>
        /// Zwraca stronę danych przesyłek dla pracownika.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<PackageDto>> PageForLoggedEmployee(PageQuery query)
        {
            string queryString = "LoggedEmployee/Page?" + query.ToQueryString();
            return await Get<PagedResult<PackageDto>>(queryString);
        }

        /// <summary>
        /// Zwraca dane pracownika przypisanego do danej paczki.
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public async Task<EmployeeDto> GetAssignedEmployee(int packageId)
        {
            return await Get<EmployeeDto>($"{packageId}/Employee");
        }

        /// <summary>
        /// Zwraca stronę danych dostępnych pracowników.
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<EmployeeDto>> GetAvailableEmployeesPage(int packageId, PageQuery query)
        {
            string queryString = $"AvailableEmployees/Page?packageId={packageId}&{query.ToQueryString()}";
            return await Get<PagedResult<EmployeeDto>>(queryString);
        }

        /// <summary>
        /// Zwraca koszt przesyłki.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public async Task<decimal> CalculatePackageCost(PackageDto package)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString[nameof(PackageDto.Height)] = package.Height.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Width)] = package.Width.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Length)] = package.Length.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Weight)] = package.Weight.ToString(CultureInfo.InvariantCulture);

            return await Get<decimal>($"Cost?{queryString}");
        }

        /// <summary>
        /// Usuwa pracownika z bazy danych.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeletePackage(int id)
        {
            await Delete(id);
        }

        /// <summary>
        /// Odpina przesyłkę od zalogowanego pracownika.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task DetachPackageFromEmployee(int employeeId)
        {
            var content = new FormUrlEncodedContent(
            new List<KeyValuePair<string, string>>
            {
                   new KeyValuePair<string, string>("", employeeId.ToString()),
            });
            await Post("DetachPackageFromEmployee", content);
        }

        /// <summary>
        /// Rejestruje paczkę.
        /// </summary>
        /// <param name="newPackage"></param>
        /// <returns></returns>
        public async Task RegisterPackage(PackageDto newPackage)
        {
            await PostAsJson("Register", newPackage);
        }

        /// <summary>
        /// Przypisuje paczkę do pracownika.
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task AssignPackage(int packageId, int employeeId)
        {
            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", employeeId.ToString()),
                });
            await Post($"Assign/{packageId}", content);
        }

        /// <summary>
        /// Oznacza paczkę jako dostarczona.
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public async Task MarkAsDelivered(int packageId)
        {
            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", packageId.ToString()),
                });
            await Post("MarkAsDelivered", content);
        }
    }
}
